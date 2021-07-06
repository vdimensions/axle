using System.Data;
using System.Data.SqlTypes;

namespace Axle.Data.SqlClient.Conversion
{
    internal sealed class SqlInt64Converter : SqlDbTypeConverter<long?, SqlInt64>
    {
        public SqlInt64Converter() : base(DbType.Int64, SqlDbType.BigInt) { }

        protected override SqlInt64 GetNotNullDestinationValue(long? value) => new SqlInt64(value.Value);
        protected override long? GetNotNullSourceValue(SqlInt64 value) => value.Value;

        protected override bool IsNull(SqlInt64 value) => value.IsNull;

        protected override long? SourceNullEquivalent => null;
        protected override SqlInt64 DestinationNullEquivalent => SqlInt64.Null;
    }
}