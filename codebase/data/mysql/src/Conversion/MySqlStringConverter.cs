using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlStringConverter : MySqlSameTypeConverter<string>
    {
        public MySqlStringConverter() : base(DbType.String, MySqlDbType.VarString, true) { }
    }
}