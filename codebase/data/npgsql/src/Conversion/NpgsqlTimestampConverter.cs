#if NETSTANDARD || NET45_OR_NEWER
using System;
using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class NpgsqlTimestampConverter : NpgsqlDbTypeConverter<DateTime?, NpgsqlDateTime?>
    {
        public NpgsqlTimestampConverter() : base(DbType.DateTime, NpgsqlDbType.Timestamp, true) { }

        protected override DateTime? GetNotNullSourceValue(NpgsqlDateTime? value) => value.Value.ToDateTime();
        protected override NpgsqlDateTime? GetNotNullDestinationValue(DateTime? value) => new NpgsqlDateTime(value.Value);

        protected override DateTime? SourceNullEquivalent => null;
        protected override NpgsqlDateTime? DestinationNullEquivalent => null;
    }
}
#endif