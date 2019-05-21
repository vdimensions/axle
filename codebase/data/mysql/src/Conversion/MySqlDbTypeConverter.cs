using System.Data;
using System.Diagnostics;
using Axle.Data.Conversion;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class MySqlDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly MySqlDbType _mySqlDbType;

        protected MySqlDbTypeConverter(DbType dbType, MySqlDbType mySqlDbType, bool registerAbstractDbType) : base(dbType)
        {
            _mySqlDbType = mySqlDbType;
            RegisterAbstractDbType = registerAbstractDbType;
        }

        public MySqlDbType MySqlDbType => _mySqlDbType;
        internal bool RegisterAbstractDbType { get; }
    }
}
