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
    internal sealed class SqliteInt64Converter : SqliteSameTypeConverter<long?>
    {
        public SqliteInt64Converter() : base(DbType.Int64, SqliteType.Integer, true) { }
    }
}