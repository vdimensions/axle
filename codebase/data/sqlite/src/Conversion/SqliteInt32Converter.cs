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
    internal sealed class SqliteInt32Converter : SqliteSameTypeConverter<int?>
    {
        public SqliteInt32Converter() : base(DbType.Int32, SqliteType.Integer, false) { }
    }
}