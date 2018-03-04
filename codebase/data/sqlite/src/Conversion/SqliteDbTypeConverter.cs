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
    internal abstract class SqliteDbTypeConverter<T> : DbTypeConverter<T, object>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SqliteType _sqliteType;

        protected SqliteDbTypeConverter(DbType dbType, SqliteType sqlType) : base(dbType)
        {
            _sqliteType = sqlType;
        }

        protected override object GetNotNullValue(T value) => value;
        protected override T GetNotNullValue(object value)
        {
            if (value is T result)
            {
                return result;
            }
            throw new System.ArgumentNullException(nameof(value));
        }

        public SqliteType SqliteType => _sqliteType;

        protected override T SourceNullEquivalent => default(T);
        protected override object DestinationNullEquivalent => null;
    }
}