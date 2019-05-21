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
    internal sealed class SqliteInt16Converter : SqliteSameTypeConverter<short?>
    {
        public SqliteInt16Converter() : base(DbType.Int16, SqliteType.Integer, false) { }
    }
}