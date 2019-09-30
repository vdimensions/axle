using System.Data;

using SqliteType = Axle.Data.Sqlite.SqliteType;


namespace Axle.Data.Sqlite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteDecimalConverter : SqliteSameTypeConverter<decimal?>
    {
        public SqliteDecimalConverter() : base(DbType.Double, SqliteType.Double, false) { }
    }
}