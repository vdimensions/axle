using System.Data;

namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteTextConverter : SQLiteSameTypeConverter<string>
    {
        public SQLiteTextConverter() : base(DbType.String, SQLiteType.Text, true) { }
    }
}