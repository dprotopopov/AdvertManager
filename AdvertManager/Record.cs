using System.ComponentModel;
using System.Reflection;
using MyLibrary.Attributes;
using MyLibrary.Collections;
using NpgsqlTypes;

namespace AdvertManager
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class Record : Properties
    {
        protected Record(Record obj):base(obj)
        {
        }

        public Record()
        {
        }

        /// <summary>
        ///     Идентификатор записи
        /// </summary>
        [NpgsqlDbType(NpgsqlDbType.Integer)]
        [Description("Идентификатор")]
        [Browsable(false)]
        [Key]
        public object Id
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

    }
}