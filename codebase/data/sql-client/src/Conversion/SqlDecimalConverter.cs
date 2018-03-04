using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlDecimalConverter : SqlDbTypeConverter<decimal?, SqlDecimal>
    {
        public SqlDecimalConverter() : base(DbType.Decimal, SqlDbType.Decimal) { }

        protected override SqlDecimal GetNotNullValue(decimal? value) => new SqlDecimal(value.Value);
        protected override decimal? GetNotNullValue(SqlDecimal value) => value.Value;

        protected override bool IsNull(SqlDecimal value) => value.IsNull;

        protected override decimal? SourceNullEquivalent => null;
        protected override SqlDecimal DestinationNullEquivalent => SqlDecimal.Null;
    }
}