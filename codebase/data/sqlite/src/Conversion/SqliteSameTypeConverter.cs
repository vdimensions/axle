using System.Data;

#if NETSTANDARD
using SqliteType = Microsoft.Data.Sqlite.SqliteType;
#else
using SqliteType = Axle.Data.Sqlite.SQLiteColumnType;
#endif


namespace Axle.Data.Sqlite.Conversion
{
    internal abstract class SqliteSameTypeConverter<T> : SqliteDbTypeConverter<T, T>
    {
        protected SqliteSameTypeConverter(DbType dbType, SqliteType npgsqlDbType, bool registerAbstractDbType) : base(dbType, npgsqlDbType, registerAbstractDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected virtual T NullEquivalent => default(T);
        protected sealed override T SourceNullEquivalent => NullEquivalent;
        protected sealed override T DestinationNullEquivalent => NullEquivalent;
    }
}