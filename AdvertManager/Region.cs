using System;
using System.ComponentModel;
using System.Reflection;
using MyLibrary.Attributes;
using NpgsqlTypes;

namespace AdvertManager
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class Region : Record
    {
        public Region(RT.Domain.Models.Region region)
        {
            if (region.Id != 0) Id = region.Id;
            Parent_Id = region.ParentId ?? (object)DBNull.Value;
            Level = region.Level;
            Has_Child = region.HasChild;
            Title = region.Title;
            Id_Path = region.IdPath;
            Modify_Date = region.ModifyDate;
        }

        public Region()
        {
        }

        public Region(Record region) : base(region)
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
        ///     ИД родительской региона
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("ИД родительской региона")]
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
        ///     Уровень региона
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("Уровень региона")]
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
        ///     Наличие дочерних узлов региона
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Boolean)]
        [Description("Наличие дочерних узлов региона")]
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
        public new object Title
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
        ///     Путь ИД региона
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Text)]
        [Description("Путь ИД региона")]
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

        public RT.Domain.Models.Region ToObject()
        {
            return new RT.Domain.Models.Region
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