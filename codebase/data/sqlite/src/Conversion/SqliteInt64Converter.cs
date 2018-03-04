using System.Data;

#if NETSTANDARD
using SqliteType = Microsoft.Data.Sqlite.SqliteType;
#else
using SqliteType = Axle.Data.Sqlite.SqliteType;
#endif


namespace Axle.Data.Sqlite.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqliteInt64Converter : SqliteSameTypeConverter<long?>
    {
        public SqliteInt64Converter() : base(DbType.Int64, SqliteType.Integer, true) { }
    }
}