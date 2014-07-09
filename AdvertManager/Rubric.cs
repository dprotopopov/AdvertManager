using System;
using System.ComponentModel;
using System.Reflection;
using MyLibrary.Attributes;
using NpgsqlTypes;
using RT.Domain.Models;

namespace AdvertManager
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class Rubric : Record
    {
        public Rubric(RT.Domain.Models.Rubric rubric)
        {
            if (rubric.Id != 0) Id = rubric.Id;
            Parent_Id = rubric.ParentId ?? (object) DBNull.Value;
            Level = rubric.Level;
            Has_Child = rubric.HasChild;
            Title = rubric.Title;
            Id_Path = rubric.IdPath;
            Modify_Date = rubric.ModifyDate;
        }

        public Rubric()
        {
        }

        public Rubric(Record rubric)
            : base(rubric)
        {
        }

        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("Идентификатор")]
        [Browsable(false)]
        [Key]
        public new object Id
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        /// <summary>
        ///     ИД родительской рубрики
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("ИД родительской рубрики")]
        [Browsable(true)]
        [Value]
        public object Parent_Id
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        /// <summary>
        ///     Уровень рубрики
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("Уровень рубрики")]
        [Browsable(true)]
        [Value]
        public object Level
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        /// <summary>
        ///     Наличие дочерних узлов рубрики
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Boolean)]
        [Description("Наличие дочерних узлов рубрики")]
        [Browsable(true)]
        [Value]
        public object Has_Child
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        /// <summary>
        ///     Заголовок
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Text)]
        [Description("Заголовок")]
        [Browsable(true)]
        [Value]
        public object Title
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        /// <summary>
        ///     Путь ИД рубрик
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Text)]
        [Description("Путь ИД рубрик")]
        [Browsable(true)]
        [Value]
        public object Id_Path
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        /// <summary>
        ///     Дата изменения
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Timestamp)]
        [Description("Дата изменения")]
        [Browsable(true)]
        [Value]
        public object Modify_Date
        {
            get
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (!ContainsKey(propertyName)) Add(propertyName, string.Empty);
                return this[propertyName];
            }
            set
            {
                string propertyName = MethodBase.GetCurrentMethod().Name.Substring(4).ToLower();
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }

        public RT.Domain.Models.Rubric ToObject()
        {
            return new RT.Domain.Models.Rubric
            {
                Id = (Id ?? DBNull.Value) == DBNull.Value ? 0 : Convert.ToInt32(Id),
                ParentId = (Parent_Id ?? DBNull.Value) == DBNull.Value ? 0 : Convert.ToInt32(Parent_Id),
                Level = Convert.ToInt16(Level),
                HasChild = Convert.ToBoolean(Has_Child),
                Title = Convert.ToString(Title),
                IdPath = Convert.ToString(Id_Path),
                ModifyDate = Convert.ToDateTime(Modify_Date),
            };
        }
    }
}