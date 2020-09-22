using System.Data;
using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal sealed class SqliteInt16Converter : SqliteSameTypeConverter<short?>
    {
        public SqliteInt16Converter() : base(DbType.Int16, SqliteType.Integer, false) { }
    }
}