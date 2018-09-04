using System;
using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlGuidConverter : SqlDbTypeConverter<Guid?, SqlGuid>
    {
        public SqlGuidConverter() : base(DbType.Guid, SqlDbType.UniqueIdentifier) { }

        protected override SqlGuid GetNotNullDestinationValue(Guid? value) => new SqlGuid(value.Value);
        protected override Guid? GetNotNullSourceValue(SqlGuid value) => value.Value;

        protected override bool IsNull(SqlGuid value) => value.IsNull;

        protected override Guid? SourceNullEquivalent => null;
        protected override SqlGuid DestinationNullEquivalent => SqlGuid.Null;
    }
}