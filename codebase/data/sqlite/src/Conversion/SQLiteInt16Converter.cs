using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteInt16Converter : SQLiteSameTypeConverter<short?>
    {
        public SQLiteInt16Converter() : base(DbType.Int16, SQLiteType.Integer, false) { }
    }
}