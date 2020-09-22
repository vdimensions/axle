using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlInt32Converter : MySqlSameTypeConverter<int?>
    {
        public MySqlInt32Converter() : base(DbType.Int32, MySqlDbType.Int32, true) { }
    }
}