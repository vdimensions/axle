#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcIntConverter : OdbcSameTypeConverter<int?>
    {
        public OdbcIntConverter() : base(DbType.Int32, OdbcType.Int, true) { }
    }
}
#endif