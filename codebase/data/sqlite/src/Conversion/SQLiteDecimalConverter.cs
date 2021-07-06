using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteDecimalConverter : SQLiteSameTypeConverter<decimal?>
    {
        public SQLiteDecimalConverter() : base(DbType.Double, SQLiteType.Double, false) { }
    }
}