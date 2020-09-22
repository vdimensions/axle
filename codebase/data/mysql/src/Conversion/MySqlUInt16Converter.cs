using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlUInt16Converter : MySqlSameTypeConverter<ushort?>
    {
        public MySqlUInt16Converter() : base(DbType.UInt16, MySqlDbType.UInt16, true) { }
    }
}