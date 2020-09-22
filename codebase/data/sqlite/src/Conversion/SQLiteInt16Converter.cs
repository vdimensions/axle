using System.Data;

namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteInt16Converter : SQLiteSameTypeConverter<short?>
    {
        public SQLiteInt16Converter() : base(DbType.Int16, SQLiteType.Integer, false) { }
    }
}