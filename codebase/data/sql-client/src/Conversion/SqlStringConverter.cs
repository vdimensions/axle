using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlStringConverter : SqlDbTypeConverter<string, SqlString>
    {
        public SqlStringConverter() : base(DbType.String, SqlDbType.NVarChar) { }

        protected override string GetNotNullSourceValue(SqlString value) => value.Value;
        protected override SqlString GetNotNullDestinationValue(string value) => new SqlString(value);

        protected override bool IsNull(SqlString value) => value.IsNull;

        protected override string SourceNullEquivalent => null;
        protected override SqlString DestinationNullEquivalent => SqlString.Null;
    }
}