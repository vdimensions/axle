using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class MySqlGuidConverter : MySqlSameTypeConverter<Guid?>
    {
        public MySqlGuidConverter() : base(DbType.Guid, MySqlDbType.Guid, true) { }
    }
}