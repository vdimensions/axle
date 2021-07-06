using System.Data;
using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal sealed class SqliteDoubleConverter : SqliteSameTypeConverter<double?>
    {
        public SqliteDoubleConverter() : base(DbType.Double, SqliteType.Real, true) { }
    }
}