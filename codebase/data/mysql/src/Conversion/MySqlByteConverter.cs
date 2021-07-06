using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlByteConverter : MySqlSameTypeConverter<byte?>
    {
        public MySqlByteConverter() : base(DbType.Byte, MySqlDbType.Byte, true) { }
    }
}