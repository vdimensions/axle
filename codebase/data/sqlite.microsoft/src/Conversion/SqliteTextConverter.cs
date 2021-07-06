using System.Data;
using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal sealed class SqliteTextConverter : SqliteSameTypeConverter<string>
    {
        public SqliteTextConverter() : base(DbType.String, SqliteType.Text, true) { }
    }
}