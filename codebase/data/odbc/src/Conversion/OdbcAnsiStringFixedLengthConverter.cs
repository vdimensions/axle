#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcAnsiStringFixedLengthConverter : OdbcSameTypeConverter<string>
    {
        public OdbcAnsiStringFixedLengthConverter() : base(DbType.AnsiStringFixedLength, OdbcType.Char, true) { }
    }
}
#endif