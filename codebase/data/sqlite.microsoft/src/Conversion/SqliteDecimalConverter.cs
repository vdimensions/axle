using System.Data;
using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal sealed class SqliteDecimalConverter : SqliteSameTypeConverter<decimal?>
    {
        public SqliteDecimalConverter() : base(DbType.Double, SqliteType.Real, false) { }
    }
}