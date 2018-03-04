#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcAnsiTextConverter : OdbcSameTypeConverter<string>
    {
        public OdbcAnsiTextConverter() : base(DbType.AnsiString, OdbcType.Text, false) { }
    }
}
#endif