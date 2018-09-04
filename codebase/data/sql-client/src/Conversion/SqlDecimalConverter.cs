using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlDecimalConverter : SqlDbTypeConverter<decimal?, SqlDecimal>
    {
        public SqlDecimalConverter() : base(DbType.Decimal, SqlDbType.Decimal) { }

        protected override SqlDecimal GetNotNullDestinationValue(decimal? value) => new SqlDecimal(value.Value);
        protected override decimal? GetNotNullSourceValue(SqlDecimal value) => value.Value;

        protected override bool IsNull(SqlDecimal value) => value.IsNull;

        protected override decimal? SourceNullEquivalent => null;
        protected override SqlDecimal DestinationNullEquivalent => SqlDecimal.Null;
    }
}