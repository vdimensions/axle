﻿#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcBigIntConverter : OdbcSameTypeConverter<long?>
    {
        public OdbcBigIntConverter() : base(DbType.Int64, OdbcType.BigInt, true) { }
    }
}
#endif