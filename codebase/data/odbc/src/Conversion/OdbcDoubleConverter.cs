#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcDoubleConverter : OdbcSameTypeConverter<double?>
    {
        public OdbcDoubleConverter() : base(DbType.Double, OdbcType.Double, true) { }
    }
}
#endif