#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcBigIntConverter : OdbcSameTypeConverter<long?>
    {
        public OdbcBigIntConverter() : base(DbType.Int64, OdbcType.BigInt, true) { }
    }
}
#endif