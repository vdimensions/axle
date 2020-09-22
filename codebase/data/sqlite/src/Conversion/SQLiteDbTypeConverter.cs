using System.Data;
using System.Diagnostics;

using Axle.Data.Conversion;

namespace Axle.Data.SQLite.Conversion
{
    internal abstract class SQLiteDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SQLiteType _sqliteType;

        protected SQLiteDbTypeConverter(DbType dbType, SQLiteType sqlType, bool registerAbstractDbType) : base(dbType)
        {
            _sqliteType = sqlType;
            RegisterAbstractDbType = registerAbstractDbType;
        }

        public SQLiteType SQLiteType => _sqliteType;

        protected override T1 SourceNullEquivalent => default(T1);
        protected override T2 DestinationNullEquivalent => default(T2);

        internal bool RegisterAbstractDbType { get; }
    }
}