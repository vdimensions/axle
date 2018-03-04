using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlInt32Converter : SqlDbTypeConverter<int?, SqlInt32>
    {
        public SqlInt32Converter() : base(DbType.Int32, SqlDbType.Int) { }

        protected override SqlInt32 GetNotNullValue(int? value) => new SqlInt32(value.Value);
        protected override int? GetNotNullValue(SqlInt32 value) => value.Value;

        protected override bool IsNull(SqlInt32 value) => value.IsNull;

        protected override int? SourceNullEquivalent => null;
        protected override SqlInt32 DestinationNullEquivalent => SqlInt32.Null;
    }
}