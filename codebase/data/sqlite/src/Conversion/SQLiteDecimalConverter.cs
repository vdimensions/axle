using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteDecimalConverter : SQLiteSameTypeConverter<decimal?>
    {
        public SQLiteDecimalConverter() : base(DbType.Double, SQLiteType.Double, false) { }
    }
}