#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcSmallDateTimeConverter : OdbcSameTypeConverter<DateTime?>
    {
        public OdbcSmallDateTimeConverter() : base(DbType.DateTime, OdbcType.SmallDateTime, false) { }
    }
}
#endif