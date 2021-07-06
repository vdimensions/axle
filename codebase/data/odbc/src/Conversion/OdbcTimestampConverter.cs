#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcTimestampConverter : OdbcSameTypeConverter<byte[]>
    {
        public OdbcTimestampConverter() : base(DbType.DateTime, OdbcType.Timestamp, false) { }
    }
}
#endif