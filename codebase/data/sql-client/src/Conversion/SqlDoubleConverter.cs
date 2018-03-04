using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlDoubleConverter : SqlDbTypeConverter<double?, SqlDouble>
    {
        public SqlDoubleConverter() : base(DbType.Double, SqlDbType.Float) { }

        protected override SqlDouble GetNotNullDestinationValue(double? value) => new SqlDouble(value.Value);
        protected override double? GetNotNullSourceValue(SqlDouble value) => value.Value;

        protected override bool IsNull(SqlDouble value) => value.IsNull;

        protected override double? SourceNullEquivalent => null;
        protected override SqlDouble DestinationNullEquivalent => SqlDouble.Null;
    }
}