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
    internal sealed class SqliteDecimalConverter : SqliteDbTypeConverter<decimal?>
    {
        #if NETSTANDARD
        public SqliteDecimalConverter() : base(DbType.Decimal, SqliteType.Real) { }
        #else
        public SqliteDecimalConverter() : base(DbType.Double, SqliteType.Double) { }
        #endif

        protected override decimal? GetNotNullValue(object value)
        {
            if (value is decimal number)
            {
                return number;
            }
            return base.GetNotNullValue(value);
        }
    }
}