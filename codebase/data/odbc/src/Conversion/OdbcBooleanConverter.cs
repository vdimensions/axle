#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcBooleanConverter : OdbcSameTypeConverter<bool?>
    {
        public OdbcBooleanConverter() : base(DbType.Boolean, OdbcType.Bit, true) { }
    }
}
#endif