using System;
using NpgsqlTypes;

namespace AdvertManager
{
    internal class NpgsqlDbTypeAttribute : Attribute
    {
        public NpgsqlDbTypeAttribute(NpgsqlDbType sqlDbType)
        {
            SqlDbType = sqlDbType;
        }

        public NpgsqlDbType SqlDbType { get; set; }
    }
}