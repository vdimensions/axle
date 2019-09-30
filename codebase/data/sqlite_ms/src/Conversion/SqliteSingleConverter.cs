using System.Data;

using SqliteType = Microsoft.Data.Sqlite.SqliteType;


namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteSingleConverter : SqliteSameTypeConverter<float?>
    {
        public SqliteSingleConverter() : base(DbType.Single, SqliteType.Real, false) { }
    }
}