using System;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class MySqlDateTimeConverter : MySqlDbTypeConverter<DateTime?, MySqlDateTime?>
    {
        public MySqlDateTimeConverter() : base(DbType.DateTime, MySqlDbType.DateTime, true) { }

        protected override DateTime? GetNotNullSourceValue(MySqlDateTime? value) => value.Value.GetDateTime();
        protected override MySqlDateTime? GetNotNullDestinationValue(DateTime? value) => new MySqlDateTime(value.Value);

        protected override DateTime? SourceNullEquivalent => null;
        protected override MySqlDateTime? DestinationNullEquivalent => null;
    }
}