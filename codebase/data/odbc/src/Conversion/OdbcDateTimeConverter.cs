﻿#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Data;
using System.Data.Odbc;


namespace Axle.Data.Odbc.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class OdbcDateTimeConverter : OdbcSameTypeConverter<DateTime?>
    {
        public OdbcDateTimeConverter() : base(DbType.DateTime, OdbcType.DateTime, true) { }
    }
}
#endif