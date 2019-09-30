using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteDoubleConverter : SQLiteSameTypeConverter<double?>
    {
        public SQLiteDoubleConverter() : base(DbType.Double, SQLiteType.Double, true) { }
    }
}