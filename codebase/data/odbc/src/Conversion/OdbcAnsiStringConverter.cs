#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class OdbcAnsiStringConverter : OdbcSameTypeConverter<string>
    {
        public OdbcAnsiStringConverter() : base(DbType.AnsiString, OdbcType.VarChar, true) { }
    }
}
#endif