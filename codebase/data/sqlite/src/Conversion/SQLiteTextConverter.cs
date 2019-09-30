using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteTextConverter : SQLiteSameTypeConverter<string>
    {
        public SQLiteTextConverter() : base(DbType.String, SQLiteType.Text, true) { }
    }
}