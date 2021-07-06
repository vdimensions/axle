using System.Data;
using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal sealed class SqliteSingleConverter : SqliteSameTypeConverter<float?>
    {
        public SqliteSingleConverter() : base(DbType.Single, SqliteType.Real, false) { }
    }
}