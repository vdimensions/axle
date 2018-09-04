#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class OdbcGuidConverter : OdbcSameTypeConverter<Guid?>
    {
        public OdbcGuidConverter() : base(DbType.Guid, OdbcType.UniqueIdentifier, true) { }
    }
}
#endif