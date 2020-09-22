using System.Data;

namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteInt32Converter : SQLiteSameTypeConverter<int?>
    {
        public SQLiteInt32Converter() : base(DbType.Int32, SQLiteType.Integer, false) { }
    }
}