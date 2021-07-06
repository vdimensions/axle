#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal sealed class OdbcSmallIntConverter : OdbcSameTypeConverter<short?>
    {
        public OdbcSmallIntConverter() : base(DbType.Int16, OdbcType.SmallInt, true) { }
    }
}
#endif