using System.Data;

#if NETSTANDARD
using SqliteType = Microsoft.Data.Sqlite.SqliteType;
#else
using SqliteType = Axle.Data.Sqlite.SQLiteColumnType;
#endif


namespace Axle.Data.Sqlite.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqliteTextConverter : SqliteSameTypeConverter<string>
    {
        public SqliteTextConverter() : base(DbType.String, SqliteType.Text, true) { }
    }
}