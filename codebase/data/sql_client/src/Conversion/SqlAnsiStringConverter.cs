using System.Data;
using System.Data.SqlTypes;

namespace Axle.Data.SqlClient.Conversion
{
    internal sealed class SqlAnsiStringConverter : SqlDbTypeConverter<string, SqlString>
    {
        public SqlAnsiStringConverter() : base(DbType.AnsiString, SqlDbType.VarChar) { }

        protected override string GetNotNullSourceValue(SqlString value) => value.Value;
        protected override SqlString GetNotNullDestinationValue(string value) => new SqlString(value);

        protected override bool IsNull(SqlString value) => value.IsNull;

        protected override string SourceNullEquivalent => null;
        protected override SqlString DestinationNullEquivalent => SqlString.Null;
    }
}