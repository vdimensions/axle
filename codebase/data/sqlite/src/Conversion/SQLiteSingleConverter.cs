using System.Data;

namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteSingleConverter : SQLiteSameTypeConverter<float?>
    {
        public SQLiteSingleConverter() : base(DbType.Single, SQLiteType.Double, false) { }
    }
}