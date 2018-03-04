using System.Data;
using System.Diagnostics;

using Axle.Data.Common.Conversion;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal abstract class SqlDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SqlDbType _sqlType;

        protected SqlDbTypeConverter(DbType dbType, SqlDbType sqlType) : base(dbType)
        {
            _sqlType = sqlType;
        }

        public SqlDbType SqlType => _sqlType;
    }
}