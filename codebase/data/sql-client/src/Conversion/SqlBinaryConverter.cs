using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlBinaryConverter : SqlDbTypeConverter<byte[], SqlBinary>
    {
        public SqlBinaryConverter() : base(DbType.Binary, SqlDbType.VarBinary) { }

        protected override byte[] GetNotNullSourceValue(SqlBinary value) => value.Value;
        protected override SqlBinary GetNotNullDestinationValue(byte[] value) => new SqlBinary(value);

        protected override bool IsNull(SqlBinary value) => value.IsNull;

        protected override byte[] SourceNullEquivalent => null;
        protected override SqlBinary DestinationNullEquivalent => SqlBinary.Null;
    }
}