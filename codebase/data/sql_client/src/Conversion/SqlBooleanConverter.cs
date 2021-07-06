using System.Data;
using System.Data.SqlTypes;

namespace Axle.Data.SqlClient.Conversion
{
    internal sealed class SqlBooleanConverter : SqlDbTypeConverter<bool?, SqlBoolean>
    {
        public SqlBooleanConverter() : base(DbType.Boolean, SqlDbType.Bit) { }

        protected override bool? GetNotNullSourceValue(SqlBoolean value) => value.Value;
        protected override SqlBoolean GetNotNullDestinationValue(bool? value) => new SqlBoolean(value.Value);

        protected override bool IsNull(SqlBoolean value) => value.IsNull;

        protected override bool? SourceNullEquivalent => null;
        protected override SqlBoolean DestinationNullEquivalent => SqlBoolean.Null;
    }
}