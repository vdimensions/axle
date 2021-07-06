#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcAnsiTextConverter : OdbcSameTypeConverter<string>
    {
        public OdbcAnsiTextConverter() : base(DbType.AnsiString, OdbcType.Text, false) { }
    }
}
#endif