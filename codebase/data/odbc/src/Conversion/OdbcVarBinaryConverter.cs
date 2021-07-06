#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcVarBinaryConverter : OdbcSameTypeConverter<byte[]>
    {
        public OdbcVarBinaryConverter() : base(DbType.Binary, OdbcType.VarBinary, true) { }
    }
}
#endif