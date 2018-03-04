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
    internal sealed class SqliteDecimalConverter : SqliteSameTypeConverter<decimal?>
    {
        #if NETSTANDARD
        public SqliteDecimalConverter() : base(DbType.Decimal, SqliteType.Real, false) { }
        #else
        public SqliteDecimalConverter() : base(DbType.Double, SqliteType.Double, false) { }
        #endif
    }
}