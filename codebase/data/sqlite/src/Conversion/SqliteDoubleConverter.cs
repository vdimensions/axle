using System.Data;

using SqliteType = Axle.Data.Sqlite.SqliteType;


namespace Axle.Data.Sqlite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteDoubleConverter : SqliteSameTypeConverter<double?>
    {
        public SqliteDoubleConverter() : base(DbType.Double, SqliteType.Double, true) { }
    }
}