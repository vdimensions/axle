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
    internal sealed class SqliteInt16Converter : SqliteSameTypeConverter<short?>
    {
        public SqliteInt16Converter() : base(DbType.Int16, SqliteType.Integer, false) { }
    }
}