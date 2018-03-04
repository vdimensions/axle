#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcBooleanConverter : OdbcSameTypeConverter<bool?>
    {
        public OdbcBooleanConverter() : base(DbType.Boolean, OdbcType.Bit, true) { }
    }
}
#endif