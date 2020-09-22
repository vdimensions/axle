using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlDoubleConverter : MySqlSameTypeConverter<double?>
    {
        public MySqlDoubleConverter() : base(DbType.Double, MySqlDbType.Double, true) { }
    }
}