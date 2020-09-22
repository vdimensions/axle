using System.Data;

namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteInt64Converter : SQLiteSameTypeConverter<long?>
    {
        public SQLiteInt64Converter() : base(DbType.Int64, SQLiteType.Integer, true) { }
    }
}