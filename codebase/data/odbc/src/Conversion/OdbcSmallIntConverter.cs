﻿#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcSmallIntConverter : OdbcSameTypeConverter<short?>
    {
        public OdbcSmallIntConverter() : base(DbType.Int16, OdbcType.SmallInt, true) { }
    }
}
#endif