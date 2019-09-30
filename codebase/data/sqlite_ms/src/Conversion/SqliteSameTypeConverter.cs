using System.Data;

using SqliteType = Microsoft.Data.Sqlite.SqliteType;


namespace Axle.Data.Sqlite.Microsoft.Conversion
{
    internal abstract class SqliteSameTypeConverter<T> : SqliteDbTypeConverter<T, T>
    {
        protected SqliteSameTypeConverter(DbType dbType, SqliteType sqliteDbType, bool registerAbstractDbType) : base(dbType, sqliteDbType, registerAbstractDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected virtual T NullEquivalent => default(T);
        protected sealed override T SourceNullEquivalent => NullEquivalent;
        protected sealed override T DestinationNullEquivalent => NullEquivalent;
    }
}