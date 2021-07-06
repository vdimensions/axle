#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcStringFixedLengthConverter : OdbcSameTypeConverter<string>
    {
        public OdbcStringFixedLengthConverter() : base(DbType.StringFixedLength, OdbcType.NChar, true) { }
    }
}
#endif