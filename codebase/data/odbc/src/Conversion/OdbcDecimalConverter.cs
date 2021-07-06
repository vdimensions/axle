#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcDecimalConverter : OdbcSameTypeConverter<decimal?>
    {
        public OdbcDecimalConverter() : base(DbType.Decimal, OdbcType.Decimal, false) { }
    }
}
#endif