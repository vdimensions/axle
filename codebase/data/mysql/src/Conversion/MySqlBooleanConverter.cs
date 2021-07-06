using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlBooleanConverter : MySqlSameTypeConverter<bool?>
    {
        public MySqlBooleanConverter() : base(DbType.Boolean, MySqlDbType.Bool, true) { }
    }
}