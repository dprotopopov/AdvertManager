using System;
using System.ComponentModel;
using System.Reflection;
using MyLibrary.Attributes;
using NpgsqlTypes;
using RT.Domain.Models;

namespace AdvertManager
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class Rubric_Cost : Record
    {
        public Rubric_Cost(RubricCost rubricCost)
        {
            if (rubricCost.Id != 0) Id = rubricCost.Id;
            Rubric_Id = rubricCost.RubricId;
            Comment = rubricCost.Comment;
            Author = rubricCost.Author;
            Cost = rubricCost.Cost;
            Modify_Date = rubricCost.ModifyDate;
        }

        public Rubric_Cost()
        {
        }

        public Rubric_Cost(Record rubricCost)
            : base(rubricCost)
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
        ///     Cost
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Money)]
        [Description("Cost")]
        [Browsable(true)]
        [Value]
        public object Cost
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
        ///     Автор
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Varchar)]
        [Description("Автор")]
        [Browsable(true)]
        [Value]
        public object Author
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
        public object Rubric_Id
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
        public object Comment
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

        public RubricCost ToObject()
        {
            return new RubricCost
            {
                Id = (Id ?? DBNull.Value) == DBNull.Value ? 0 : Convert.ToInt32(Id),
                RubricId = (Rubric_Id ?? DBNull.Value) == DBNull.Value ? 0 : Convert.ToInt32(Rubric_Id),
                Comment = Convert.ToString(Comment),
                Author = Convert.ToString(Author),
                Cost = Convert.ToDecimal(Cost),
                ModifyDate = Convert.ToDateTime(Modify_Date),
            };
        }
    }
}