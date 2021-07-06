using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlStringFixedLengthConverter : MySqlSameTypeConverter<string>
    {
        public MySqlStringFixedLengthConverter() : base(DbType.StringFixedLength, MySqlDbType.String, true) { }
    }
}