using System.Data;

using SqliteType = Axle.Data.Sqlite.SqliteType;


namespace Axle.Data.Sqlite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteSingleConverter : SqliteSameTypeConverter<float?>
    {
        public SqliteSingleConverter() : base(DbType.Single, SqliteType.Double, false) { }
    }
}