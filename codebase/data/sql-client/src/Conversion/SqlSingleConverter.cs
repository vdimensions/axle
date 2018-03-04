using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlSingleConverter : SqlDbTypeConverter<float?, SqlSingle>
    {
        public SqlSingleConverter() : base(DbType.Single, SqlDbType.Real) { }

        protected override SqlSingle GetNotNullValue(float? value) => value.Value;
        protected override float? GetNotNullValue(SqlSingle value) => value.Value;

        protected override bool IsNull(SqlSingle value) => value.IsNull;

        protected override float? SourceNullEquivalent => null;
        protected override SqlSingle DestinationNullEquivalent => SqlSingle.Null;
    }
}