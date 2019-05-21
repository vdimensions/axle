using System.Data;

#if NETFRAMEWORK
using SqliteType = Axle.Data.Sqlite.SqliteType;
#else
using SqliteType = Microsoft.Data.Sqlite.SqliteType;
#endif


namespace Axle.Data.Sqlite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteDecimalConverter : SqliteSameTypeConverter<decimal?>
    {
        #if NETFRAMEWORK
        public SqliteDecimalConverter() : base(DbType.Double, SqliteType.Double, false) { }
        #else
        public SqliteDecimalConverter() : base(DbType.Decimal, SqliteType.Real, false) { }
        #endif
    }
}