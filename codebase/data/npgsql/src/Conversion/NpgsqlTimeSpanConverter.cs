#if NETSTANDARD || NET45_OR_NEWER
using System;
using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlTimeSpanConverter : NpgsqlDbTypeConverter<TimeSpan?, NpgsqlTimeSpan?>
    {
        public NpgsqlTimeSpanConverter() : base(DbType.DateTimeOffset, NpgsqlDbType.Interval) { }

        protected override TimeSpan? GetNotNullSourceValue(NpgsqlTimeSpan? value) => value.Value.Time;
        protected override NpgsqlTimeSpan? GetNotNullDestinationValue(TimeSpan? value) => new NpgsqlTimeSpan(value.Value);

        protected override TimeSpan? SourceNullEquivalent => null;
        protected override NpgsqlTimeSpan? DestinationNullEquivalent => null;
    }
}
#endif