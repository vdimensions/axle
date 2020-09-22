using System.Data;

namespace Axle.Data.SQLite.Conversion
{
    internal abstract class SQLiteSameTypeConverter<T> : SQLiteDbTypeConverter<T, T>
    {
        protected SQLiteSameTypeConverter(DbType dbType, SQLiteType sqliteDbType, bool registerAbstractDbType) : base(dbType, sqliteDbType, registerAbstractDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected virtual T NullEquivalent => default(T);
        protected sealed override T SourceNullEquivalent => NullEquivalent;
        protected sealed override T DestinationNullEquivalent => NullEquivalent;
    }
}