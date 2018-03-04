#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;

using Axle.Data.Common.Conversion;


namespace Axle.Data.Odbc.Conversion
{
    [Serializable]
    internal abstract class OdbcTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly OdbcType _odbcType;

        protected OdbcTypeConverter(DbType dbType, OdbcType odbcType) : base(dbType)
        {
            _odbcType = odbcType;
        }

        public OdbcType OdbcType => _odbcType;
    }
}
#endif