using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteInt64Converter : SQLiteSameTypeConverter<long?>
    {
        public SQLiteInt64Converter() : base(DbType.Int64, SQLiteType.Integer, true) { }
    }
}