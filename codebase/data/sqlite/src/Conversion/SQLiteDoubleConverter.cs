using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteDoubleConverter : SQLiteSameTypeConverter<double?>
    {
        public SQLiteDoubleConverter() : base(DbType.Double, SQLiteType.Double, true) { }
    }
}