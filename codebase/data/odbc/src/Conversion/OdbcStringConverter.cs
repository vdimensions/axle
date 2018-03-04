#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcStringConverter : OdbcSameTypeConverter<string>
    {
        public OdbcStringConverter() : base(DbType.String, OdbcType.NVarChar, true) { }
    }
}
#endif