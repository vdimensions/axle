using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlByteConverter : SqlDbTypeConverter<byte?, SqlByte>
    {
        public SqlByteConverter() : base(DbType.Byte, SqlDbType.TinyInt) { }

        protected override byte? GetNotNullSourceValue(SqlByte value) => value.Value;
        protected override SqlByte GetNotNullDestinationValue(byte? value) => new SqlByte(value.Value);

        protected override bool IsNull(SqlByte value) => value.IsNull;

        protected override byte? SourceNullEquivalent => null;
        protected override SqlByte DestinationNullEquivalent => SqlByte.Null;
    }
}