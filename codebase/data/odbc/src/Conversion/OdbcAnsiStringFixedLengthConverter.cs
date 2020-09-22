#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcAnsiStringFixedLengthConverter : OdbcSameTypeConverter<string>
    {
        public OdbcAnsiStringFixedLengthConverter() : base(DbType.AnsiStringFixedLength, OdbcType.Char, true) { }
    }
}
#endif