using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

using Axle.Data.Common.Conversion;


namespace Axle.Data.OleDb.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal abstract class OleDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly OleDbType _oleDbType;

        protected OleDbTypeConverter(DbType dbType, OleDbType oleDbType) : base(dbType)
        {
            _oleDbType = oleDbType;
        }

        public OleDbType OleDbType => _oleDbType;
    }
}
