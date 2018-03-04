using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlAnsiStringConverter : SqlDbTypeConverter<string, SqlString>
    {
        public SqlAnsiStringConverter() : base(DbType.AnsiString, SqlDbType.VarChar) { }

        protected override SqlString GetNotNullValue(string value) => new SqlString(value);
        protected override string GetNotNullValue(SqlString value) => value.Value;

        protected override bool IsNull(SqlString value) => value.IsNull;

        protected override string SourceNullEquivalent => null;
        protected override SqlString DestinationNullEquivalent => SqlString.Null;
    }
}