using System.Data;

#if NETSTANDARD
using SqliteType = Microsoft.Data.Sqlite.SqliteType;
#else
using SqliteType = Axle.Data.Sqlite.SQLiteColumnType;
#endif


namespace Axle.Data.Sqlite.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqliteSingleConverter : SqliteDbTypeConverter<float?>
    {
        #if NETSTANDARD
        public SqliteSingleConverter() : base(DbType.Double, SqliteType.Real) { }
        #else
        public SqliteSingleConverter() : base(DbType.Double, SqliteType.Double) { }
        #endif

        protected override float? GetNotNullValue(object value)
        {
            if (value is float number)
            {
                return number;
            }
            return base.GetNotNullValue(value);
        }
    }
}