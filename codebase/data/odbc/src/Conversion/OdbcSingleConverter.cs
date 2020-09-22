#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcSingleConverter : OdbcSameTypeConverter<float?>
    {
        public OdbcSingleConverter() : base(DbType.Single, OdbcType.Real, true) { }
    }
}
#endif