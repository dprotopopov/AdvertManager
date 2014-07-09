using System;
using System.ComponentModel;
using System.Reflection;
using MyLibrary.Attributes;
using NpgsqlTypes;

namespace AdvertManager
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class Action : Record
    {
        public Action(RT.Domain.Models.Action action)
        {
            if (action.Id != 0) Id = action.Id;
            Anti_Action_Id = action.AntiActionId ?? (object)DBNull.Value;
            Title = action.Title;
            Modify_Date = action.ModifyDate;
        }

        public Action()
        {
        }

        public Action(Record action)
            : base(action)
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
        ///     Противоположное действие
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("Противоположное действие")]
        [Browsable(true)]
        [Value]
        public object Anti_Action_Id
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

        public RT.Domain.Models.Action ToObject()
        {
            return new RT.Domain.Models.Action
            {
                Id = (Id ?? DBNull.Value) == DBNull.Value ? 0 : Convert.ToInt32(Id),
                AntiActionId = (Anti_Action_Id ?? DBNull.Value) == DBNull.Value ? 0 : Convert.ToInt32(Anti_Action_Id),
                Title = Convert.ToString(Title),
                ModifyDate =Convert.ToDateTime(Modify_Date),
            };
        }
    }
}