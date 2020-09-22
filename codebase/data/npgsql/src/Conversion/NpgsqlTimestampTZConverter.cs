#if NETSTANDARD || NET45_OR_NEWER
using System;
using System.Data;
using NpgsqlTypes;

namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlTimestampTZConverter : NpgsqlDbTypeConverter<DateTime?, NpgsqlDateTime?>
    {
        public NpgsqlTimestampTZConverter() : base(DbType.DateTime, NpgsqlDbType.TimestampTZ, false) { }

        protected override DateTime? GetNotNullSourceValue(NpgsqlDateTime? value) => value.Value.ToDateTime();
        protected override NpgsqlDateTime? GetNotNullDestinationValue(DateTime? value) => new NpgsqlDateTime(value.Value);

        protected override DateTime? SourceNullEquivalent => null;
        protected override NpgsqlDateTime? DestinationNullEquivalent => null;
    }
}
#endif