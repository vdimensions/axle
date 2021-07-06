#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;

using Axle.Data.Conversion;

namespace Axle.Data.Odbc.Conversion
{
    internal abstract class OdbcTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly OdbcType _odbcType;

        protected OdbcTypeConverter(DbType dbType, OdbcType odbcType, bool registerAbstractDbType) : base(dbType)
        {
            _odbcType = odbcType;
            RegisterAbstractDbType = registerAbstractDbType;
        }

        public OdbcType OdbcType => _odbcType;
        internal bool RegisterAbstractDbType { get; }
    }
}
#endif