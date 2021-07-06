#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcTextConverter : OdbcSameTypeConverter<string>
    {
        public OdbcTextConverter() : base(DbType.String, OdbcType.NText, false) { }
    }
}
#endif