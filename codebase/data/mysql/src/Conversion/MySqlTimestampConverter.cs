using System;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class MySqlTimestampConverter : MySqlDbTypeConverter<DateTime?, MySqlDateTime?>
    {
        public MySqlTimestampConverter() : base(DbType.DateTime, MySqlDbType.Timestamp, true) { }

        protected override DateTime? GetNotNullSourceValue(MySqlDateTime? value)
        {
            var v = value.Value;
            return new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, v.Second, v.Millisecond);
        }

        protected override MySqlDateTime? GetNotNullDestinationValue(DateTime? value) => new MySqlDateTime(value.Value);

        protected override DateTime? SourceNullEquivalent => null;
        protected override MySqlDateTime? DestinationNullEquivalent => null;
    }
}