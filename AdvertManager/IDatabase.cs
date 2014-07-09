using System.Collections.Generic;
using Npgsql;

namespace AdvertManager
{
    public interface IDatabase
    {
        string ConnectionString { get; set; }

        /// <summary>
        ///     Коннектор к базе данных
        /// </summary>
        NpgsqlConnection Connection { get; }

        bool Wait(NpgsqlConnection connection);

        void Release(NpgsqlConnection connection);
        void Connect();

        IEnumerable<Record> Load(Record maskRecord, string compare = "=");
        void Delete(Record maskRecord, string compare = "=");
        bool Exists(Record maskRecord, string compare = "=");
        int InsertOrReplace(IEnumerable<Record> records);
        int InsertOrReplace(Record record);
        int ExecuteNonQuery(string commandText);
        int InsertIfNotExists(Record record);

    }
}