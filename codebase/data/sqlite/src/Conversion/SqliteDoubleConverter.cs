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
    internal sealed class SqliteDoubleConverter : SqliteDbTypeConverter<double?>
    {
        #if NETSTANDARD
        public SqliteDoubleConverter() : base(DbType.Double, SqliteType.Real) { }
        #else
        public SqliteDoubleConverter() : base(DbType.Double, SqliteType.Double) { }
        #endif
    }
}