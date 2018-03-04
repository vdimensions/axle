using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal abstract class NpgsqlSameTypeConverter<T> : NpgsqlDbTypeConverter<T, T>
    {
        protected NpgsqlSameTypeConverter(DbType dbType, NpgsqlDbType npgsqlDbType, bool registerAbstractDbType) : base(dbType, npgsqlDbType, registerAbstractDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected override T SourceNullEquivalent => default(T);
        protected override T DestinationNullEquivalent => default(T);
    }
}