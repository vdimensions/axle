using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlBooleanConverter : SqlDbTypeConverter<bool?, SqlBoolean>
    {
        public SqlBooleanConverter() : base(DbType.Boolean, SqlDbType.Bit) { }

        protected override SqlBoolean GetNotNullValue(bool? value) => new SqlBoolean(value.Value);
        protected override bool? GetNotNullValue(SqlBoolean value) => value.Value;

        protected override bool IsNull(SqlBoolean value) => value.IsNull;

        protected override bool? SourceNullEquivalent => null;
        protected override SqlBoolean DestinationNullEquivalent => SqlBoolean.Null;
    }
}