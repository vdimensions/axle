#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcTextConverter : OdbcSameTypeConverter<string>
    {
        public OdbcTextConverter() : base(DbType.String, OdbcType.NText, false) { }
    }
}
#endif