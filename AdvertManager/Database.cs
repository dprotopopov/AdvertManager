using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using MyLibrary;
using MyLibrary.Attributes;
using MyLibrary.Collections;
using MyLibrary.LastError;
using MyLibrary.Lock;
using Npgsql;

namespace AdvertManager
{
    /// <summary>
    ///     Класс для работы с базой данных
    /// </summary>
    public class Database : IDatabase, ILastError, ISingleLock<NpgsqlConnection>
    {
        public static Database Persistent = new Database();

        #region

        private Mutex Mutex { get; set; }

        public bool Wait(NpgsqlConnection semaphore)
        {
            while (!Mutex.WaitOne(0)) Thread.Sleep(1000);
            return true;
        }

        public void Release(NpgsqlConnection semaphore)
        {
            Mutex.ReleaseMutex();
        }

        public bool Wait(NpgsqlConnection semaphore, TimeSpan timeout)
        {
            return Mutex.WaitOne(timeout);
        }

        #endregion

        public Database()
        {
            ConnectionString = "User Id=postgres;Password=postgres;Host=localhost;Database=advertmanager;";
            Connection = new NpgsqlConnection(ConnectionString);
            Semaphore = new object();
            Mutex = new Mutex();
            Connect();
        }

        /// <summary>
        ///     Семафор для блокирования одновременного доступа к классу из параллельных процессов
        ///     Использование lock(database.Semaphore) { ... }
        /// </summary>
        public object Semaphore { get; set; }

        /// <summary>
        ///     Строка для подключения к базе данных
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Коннектор к базе данных
        /// </summary>
        public NpgsqlConnection Connection { get; set; }

        /// <summary>
        ///     Метод инициализации к базе данных
        /// </summary>
        public void Connect()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Connection.Open();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        ///     Добавление записей в базу данных.
        ///     При совпадении ключевых полей с существующими записями, существующая запись перезаписывается
        /// </summary>
        /// <param name="records">Список добавляемых записей</param>
        /// <returns>Количество добавленных или изменённых записей</returns>
        public int InsertOrReplace(IEnumerable<Record> records)
        {
            int insertOrReplace = 0;
            foreach (Record record in records)
                insertOrReplace += InsertOrReplace(record);
            return insertOrReplace;
        }

        public int InsertOrReplace(Record record)
        {
            int insertOrReplace = 0;
            string where = string.Join(" AND ",
                record.GetType()
                    .GetProperties()
                    .Where(prop => prop.GetCustomAttributes(typeof (KeyAttribute)).Any())
                    .Select(prop => prop.Name)
                    .Where(key => record.ContainsKey(key.ToLower()))
                    .Select(key => string.Format("{0}=:{0}", key.ToLower())));
            var propertyInfos = new StackListQueue<PropertyInfo>(record.GetType().GetProperties()
                .Where(prop => record.ContainsKey(prop.Name.ToLower())));

            using (NpgsqlCommand command = Connection.CreateCommand())
            {
                command.CommandText =
                    string.Format(
                        string.IsNullOrWhiteSpace(where)
                            ? "INSERT INTO {0}({1}) SELECT {2}"
                            : "INSERT INTO {0}({1}) SELECT {2} WHERE NOT EXISTS (SELECT * FROM {0} {4});UPDATE {0} SET {3} {4};",
                        record.GetType().Name.ToLower(),
                        string.Join(",", propertyInfos.Select(prop => prop.Name.ToLower())),
                        string.Join(",", propertyInfos.Select(prop => string.Format(":{0}", prop.Name.ToLower()))),
                        string.Join(",", propertyInfos.Select(prop => string.Format("{0}=:{0}", prop.Name.ToLower()))),
                        string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format("WHERE {0}", where));
                Debug.WriteLine(command.CommandText);
                foreach (PropertyInfo prop in propertyInfos)
                    command.Parameters.Add(new NpgsqlParameter(string.Format(":{0}", prop.Name.ToLower()),
                        record[prop.Name.ToLower()]));
                insertOrReplace += command.ExecuteNonQuery();
            }
            return insertOrReplace;
        }

        /// <summary>
        ///     Выполнение SQL кода в базе данных
        /// </summary>
        /// <param name="commandText">Текст SQL кода</param>
        /// <returns>Код, возвращаемый командой ExecuteNonQuery</returns>
        public int ExecuteNonQuery(string commandText)
        {
            int executeNonQuery = 0;
            using (NpgsqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = commandText;
                Debug.WriteLine(command.CommandText);
                executeNonQuery = command.ExecuteNonQuery();
            }
            return executeNonQuery;
        }

        public int InsertIfNotExists(Record record)
        {
            return Exists(record) ? 0 : InsertOrReplace(record);
        }

        /// <summary>
        ///     Проверка существования записей в базе данных с указанным критерием
        /// </summary>
        /// <param name="maskRecord">Параметры критерия поиска записей</param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public bool Exists(Record maskRecord, string compare = "=")
        {
            bool value = false;
            var propertyInfos = new StackListQueue<PropertyInfo>(maskRecord.GetType().GetProperties()
                .Where(prop => maskRecord.ContainsKey(prop.Name.ToLower())));

            using (NpgsqlCommand command = Connection.CreateCommand())
            {
                string where = string.Join(" AND ",
                    propertyInfos.Select(prop => string.Format("{0}{1}:{0}", prop.Name.ToLower(), compare)));
                command.CommandText =
                    string.Format("SELECT * FROM {0} {1} LIMIT 1", maskRecord.GetType().Name.ToLower(),
                        string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format("WHERE {0}", where));
                Debug.WriteLine(command.CommandText);
                foreach (PropertyInfo prop in propertyInfos)
                    command.Parameters.Add(new NpgsqlParameter(string.Format(":{0}", prop.Name.ToLower()),
                        maskRecord[prop.Name.ToLower()]));
                NpgsqlDataReader reader = command.ExecuteReader();
                value = reader.HasRows;
            }
            return value;
        }

        public IEnumerable<Record> Load(Record maskRecord, string compare = "=")
        {
            var list = new StackListQueue<Record>();
            var propertyInfos = new StackListQueue<PropertyInfo>(maskRecord.GetType().GetProperties()
                .Where(prop => maskRecord.ContainsKey(prop.Name.ToLower())));

            using (NpgsqlCommand command = Connection.CreateCommand())
            {
                string where = string.Join(" AND ",
                    propertyInfos.Select(prop => string.Format("{0}{1}:{0}", prop.Name.ToLower(), compare)));
                command.CommandText =
                    string.Format("SELECT * FROM {0} {1}", maskRecord.GetType().Name.ToLower(),
                        string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format("WHERE {0}", where));
                Debug.WriteLine(command.CommandText);
                foreach (PropertyInfo prop in propertyInfos)
                {
                    command.Parameters.Add(new NpgsqlParameter(string.Format(":{0}", prop.Name.ToLower()),
                        maskRecord[prop.Name.ToLower()]));
                }
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var record = new Record();
                    for (int i = 0; i < reader.FieldCount; i++)
                        record.Add(reader.GetName(i), reader[i]);
                    list.Add(record);
                }
            }
            return list;
        }

        /// <summary>
        ///     Удаление из базы данных всех записей, удовлетворяющих указанному критерию
        /// </summary>
        /// <param name="maskRecord"></param>
        /// <param name="compare"></param>
        public void Delete(Record maskRecord, string compare = "=")
        {
            var propertyInfos = new StackListQueue<PropertyInfo>(maskRecord.GetType().GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(KeyAttribute)).Any())
                .Where(prop => maskRecord.ContainsKey(prop.Name.ToLower())));
            using (NpgsqlCommand command = Connection.CreateCommand())
            {
                string where = string.Join(" AND ",
                    propertyInfos.Select(prop => string.Format("{0}{1}:{0}", prop.Name.ToLower(), compare)));
                command.CommandText =
                    string.Format("DELETE FROM {0} {1}", maskRecord.GetType().Name.ToLower(),
                        string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format("WHERE {0}", where));
                Debug.WriteLine(command.CommandText);
                foreach (PropertyInfo prop in propertyInfos)
                    command.Parameters.Add(new NpgsqlParameter(string.Format(":{0}", prop.Name.ToLower()),
                        maskRecord[prop.Name.ToLower()]));
                command.ExecuteNonQuery();
            }
        }

        public object LastError { get; set; }

        /// <summary>
        ///     Метод отключения от базы данных
        /// </summary>
        public void Disconnect()
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            Connection.Close();
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        ///     Добавление записи в таблицу
        /// </summary>
        /// <param name="record">Добавляемая запись</param>
        private void Add(Record record)
        {
            Debug.WriteLine("Begin {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
            var list = new StackListQueue<string>(record.GetType()
                .GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute(typeof (ValueAttribute), false) != null)
                .Select(propInfo => propInfo.Name.ToLower()));
            string text = string.Format("INSERT INTO {0}({1}) VALUES({2})", record.GetType().Name.ToLower(),
                string.Join(",", list.Select(s => string.Format("{0}", s.ToLower()))),
                string.Join(",", list.Select(s => string.Format(":{0}", s.ToLower()))));
            Debug.WriteLine(text);
            using (var command = new NpgsqlCommand(text, Connection))
            {
                foreach (string s in list)
                {
                    PropertyInfo propertyInfo = record.GetType()
                        .GetProperty(s, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    var customAttribute =
                        (NpgsqlDbTypeAttribute)
                            propertyInfo.GetCustomAttribute(typeof (NpgsqlDbTypeAttribute), false);
                    NpgsqlParameter param = command.Parameters.Add(s.ToLower(), customAttribute.SqlDbType);
                    param.Value = propertyInfo.GetValue(record, null);
                }
                command.ExecuteNonQuery();
            }
            Debug.WriteLine("End {0}::{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public Values ToValues()
        {
            return new Values(this);
        }
    }
}