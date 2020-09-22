using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlUInt64Converter : MySqlSameTypeConverter<ulong?>
    {
        public MySqlUInt64Converter() : base(DbType.UInt64, MySqlDbType.UInt64, true) { }
    }
}