using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlStringFixedLengthConverter : SqlDbTypeConverter<string, SqlString>
    {
        public SqlStringFixedLengthConverter() : base(DbType.StringFixedLength, SqlDbType.NChar) { }

        protected override string GetNotNullSourceValue(SqlString value) => value.Value;
        protected override SqlString GetNotNullDestinationValue(string value) => new SqlString(value);

        protected override bool IsNull(SqlString value) => value.IsNull;

        protected override string SourceNullEquivalent => null;
        protected override SqlString DestinationNullEquivalent => SqlString.Null;
    }
}