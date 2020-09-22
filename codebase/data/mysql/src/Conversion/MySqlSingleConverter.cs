using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlSingleConverter : MySqlSameTypeConverter<float?>
    {
        public MySqlSingleConverter() : base(DbType.Single, MySqlDbType.Float, true) { }
    }
}