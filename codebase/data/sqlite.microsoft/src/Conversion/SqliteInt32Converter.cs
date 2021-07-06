using System.Data;
using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal sealed class SqliteInt32Converter : SqliteSameTypeConverter<int?>
    {
        public SqliteInt32Converter() : base(DbType.Int32, SqliteType.Integer, false) { }
    }
}