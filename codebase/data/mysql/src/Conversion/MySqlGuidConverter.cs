using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql.Conversion
{
    internal sealed class MySqlGuidConverter : MySqlSameTypeConverter<Guid?>
    {
        public MySqlGuidConverter() : base(DbType.Guid, MySqlDbType.Guid, true) { }
    }
}