using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlInt16Converter : SqlDbTypeConverter<short?, SqlInt16>
    {
        public SqlInt16Converter() : base(DbType.Int16, SqlDbType.SmallInt) { }

        protected override SqlInt16 GetNotNullValue(short? value) => new SqlInt16(value.Value);
        protected override short? GetNotNullValue(SqlInt16 value) => value.Value;

        protected override bool IsNull(SqlInt16 value) => value.IsNull;

        protected override short? SourceNullEquivalent => null;
        protected override SqlInt16 DestinationNullEquivalent => SqlInt16.Null;
    }
}