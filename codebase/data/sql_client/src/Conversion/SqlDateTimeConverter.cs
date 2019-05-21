using System;
using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlDateTimeConverter : SqlDbTypeConverter<DateTime?, SqlDateTime>
    {
        public SqlDateTimeConverter() : base(DbType.DateTime, SqlDbType.DateTime) { }

        protected override SqlDateTime GetNotNullDestinationValue(DateTime? value) => new SqlDateTime(value.Value);
        protected override DateTime? GetNotNullSourceValue(SqlDateTime value) => value.Value;

        protected override bool IsNull(SqlDateTime value) => value.IsNull;

        protected override DateTime? SourceNullEquivalent => null;
        protected override SqlDateTime DestinationNullEquivalent => SqlDateTime.Null;
    }
}