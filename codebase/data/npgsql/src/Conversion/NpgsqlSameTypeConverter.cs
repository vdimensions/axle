using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    internal abstract class NpgsqlSameTypeConverter<T> : NpgsqlDbTypeConverter<T, T>
    {
        protected NpgsqlSameTypeConverter(DbType dbType, NpgsqlDbType npgsqlDbType) : base(dbType, npgsqlDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected override T SourceNullEquivalent => default(T);
        protected override T DestinationNullEquivalent => default(T);
    }
}