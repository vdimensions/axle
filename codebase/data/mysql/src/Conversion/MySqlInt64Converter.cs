using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlInt64Converter : MySqlSameTypeConverter<long?>
    {
        public MySqlInt64Converter() : base(DbType.Int64, MySqlDbType.Int64, true) { }
    }
}