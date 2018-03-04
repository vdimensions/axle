#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcDecimalConverter : OdbcSameTypeConverter<decimal?>
    {
        public OdbcDecimalConverter() : base(DbType.Decimal, OdbcType.Decimal, false) { }
    }
}
#endif