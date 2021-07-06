#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcNumericConverter : OdbcSameTypeConverter<decimal?>
    {
        public OdbcNumericConverter() : base(DbType.VarNumeric, OdbcType.Numeric, true) { }
    }
}
#endif