using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteSingleConverter : SQLiteSameTypeConverter<float?>
    {
        public SQLiteSingleConverter() : base(DbType.Single, SQLiteType.Double, false) { }
    }
}