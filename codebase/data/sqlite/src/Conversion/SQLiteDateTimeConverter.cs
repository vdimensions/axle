using System;
using System.Data;
using System.Globalization;
using Axle.Conversion.Parsing;

namespace Axle.Data.SQLite.Conversion
{
    internal sealed class SQLiteDateTimeConverter : SQLiteDbTypeConverter<DateTime?, string>
    {
        private const string DateFormat = "yyyy-MM-dd hh:mm:ss.fff";
        private readonly IStrictParser<DateTime> _parser = new DateTimeParser(); 
        
        public SQLiteDateTimeConverter() : base(DbType.DateTime, SQLiteType.Text, false) { }
        
        protected override DateTime? GetNotNullSourceValue(string value)
        {
            return value == null 
                ? null 
                : new DateTime?(_parser.ParseExact(value, DateFormat, CultureInfo.InvariantCulture));
        }

        protected override string GetNotNullDestinationValue(DateTime? value)
        {
            return $"datetime('{value.Value.ToString(DateFormat, CultureInfo.InvariantCulture)}')";
        }
    }
}