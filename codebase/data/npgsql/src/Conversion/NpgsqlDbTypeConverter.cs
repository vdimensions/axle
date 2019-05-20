using System.Data;
using System.Diagnostics;

using Axle.Data.Conversion;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class NpgsqlDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly NpgsqlDbType _npgsqlType;

        protected NpgsqlDbTypeConverter(DbType dbType, NpgsqlDbType npgsqlType, bool registerAbstractDbType) : base(dbType)
        {
            _npgsqlType = npgsqlType;
            RegisterAbstractDbType = registerAbstractDbType;
        }

        public NpgsqlDbType NpgsqlType => _npgsqlType;
        internal bool RegisterAbstractDbType { get; }
    }
}