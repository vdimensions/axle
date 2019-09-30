using System.Data;
using System.Diagnostics;

using Axle.Data.Conversion;

using SqliteType = Axle.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class SqliteDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SqliteType _sqliteType;

        private readonly bool _registerAbstractDbType;

        protected SqliteDbTypeConverter(DbType dbType, SqliteType sqlType, bool registerAbstractDbType) : base(dbType)
        {
            _sqliteType = sqlType;
            _registerAbstractDbType = registerAbstractDbType;
        }

        public SqliteType SqliteType => _sqliteType;

        protected override T1 SourceNullEquivalent => default(T1);
        protected override T2 DestinationNullEquivalent => default(T2);

        internal bool RegisterAbstractDbType => _registerAbstractDbType;
    }
}