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
    internal sealed class SqliteSingleConverter : SqliteSameTypeConverter<float?>
    {
        #if NETSTANDARD
        public SqliteSingleConverter() : base(DbType.Single, SqliteType.Real, false) { }
        #else
        public SqliteSingleConverter() : base(DbType.Single, SqliteType.Double, false) { }
        #endif
    }
}