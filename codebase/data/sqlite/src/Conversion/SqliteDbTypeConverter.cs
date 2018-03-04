using System.Data;
using System.Diagnostics;

using Axle.Data.Common.Conversion;

#if NETSTANDARD
using SqliteType = Microsoft.Data.Sqlite.SqliteType;
#else
using SqliteType = Axle.Data.Sqlite.SQLiteColumnType;
#endif

namespace Axle.Data.Sqlite.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal abstract class SqliteDbTypeConverter<T> : DbTypeConverter<T, T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SqliteType _sqliteType;

        protected SqliteDbTypeConverter(DbType dbType, SqliteType sqlType) : base(dbType)
        {
            _sqliteType = sqlType;
        }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        public SqliteType SqliteType => _sqliteType;

        protected virtual T NullEquivalent => default(T);
        protected override T SourceNullEquivalent => NullEquivalent;
        protected override T DestinationNullEquivalent => NullEquivalent;
    }
}