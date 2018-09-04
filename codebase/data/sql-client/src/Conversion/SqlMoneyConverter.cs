using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlMoneyConverter : SqlDbTypeConverter<decimal?, SqlMoney>
    {
        public SqlMoneyConverter() : base(DbType.Currency, SqlDbType.Money) { }

        protected override SqlMoney GetNotNullDestinationValue(decimal? value) => new SqlMoney(value.Value);
        protected override decimal? GetNotNullSourceValue(SqlMoney value) => value.Value;

        protected override bool IsNull(SqlMoney value) => value.IsNull;

        protected override decimal? SourceNullEquivalent => null;
        protected override SqlMoney DestinationNullEquivalent => SqlMoney.Null;
    }
}