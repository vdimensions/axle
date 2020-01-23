using System.Data;

using SqliteType = Microsoft.Data.Sqlite.SqliteType;


namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteInt64Converter : SqliteSameTypeConverter<long?>
    {
        public SqliteInt64Converter() : base(DbType.Int64, SqliteType.Integer, true) { }
    }
}