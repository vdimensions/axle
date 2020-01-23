using System.Data;
using System.Diagnostics;

using Axle.Data.Conversion;

using SqliteType = Microsoft.Data.Sqlite.SqliteType;

namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class SqliteDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SqliteType _sqliteType;

        protected SqliteDbTypeConverter(DbType dbType, SqliteType sqlType, bool registerAbstractDbType) : base(dbType)
        {
            _sqliteType = sqlType;
            RegisterAbstractDbType = registerAbstractDbType;
        }

        public SqliteType SqliteType => _sqliteType;

        protected override T1 SourceNullEquivalent => default(T1);
        protected override T2 DestinationNullEquivalent => default(T2);

        internal bool RegisterAbstractDbType { get; }
    }
}