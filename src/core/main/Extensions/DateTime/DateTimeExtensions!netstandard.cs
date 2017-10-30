using System;


namespace Axle.Extensions.DateTime
{
    using DateTime = System.DateTime;

    /// <summary>
    /// A static class that contains extension methods for the <see cref="DateTime"/> struct
    /// </summary>
    public static partial class DateTimeExtensions
    {
        public static DateTime ToLocalTime(this DateTime current, DateTimeKind assumedKind)
        {
            var kind = current.Kind;
            return (kind == DateTimeKind.Unspecified ? assumedKind : kind) == DateTimeKind.Utc 
                ? TimeZoneInfo.ConvertTime(current, TimeZoneInfo.Utc, TimeZoneInfo.Local) 
                : new DateTime(current.Ticks, DateTimeKind.Local);
        }
        public static DateTime ToLocalTime(this DateTime current) { return ToLocalTime(current, DateTimeKind.Local); }

        /// <summary>
        /// Converts a time from one time zone to another. 
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> instance upon which this extension method is invoked.
        /// </param>
        /// <param name="sourceTimeZone">
        /// The time zone of the given <paramref name="dateTime"/>. 
        /// </param>
        /// <param name="destinationTimeZone">
        /// The time zone to convert <paramref name="dateTime"/> to.
        /// </param>
        /// <param name="assumeLocal">
        /// A boolean value indicating whether the provided <see cref="DateTime"/> value in the
        /// <paramref name="dateTime"/> should be treated as a local time in the <paramref name="sourceTimeZone"/>
        /// if its <see cref="DateTime.Kind" /> property is set to <see cref="DateTimeKind.Unspecified"/>.
        /// If this value is <c>false</c>, the <paramref name="dateTime"/> is assumed to be an UTC date, otherwise
        /// it is treated as a date local to the timezone specified by the <paramref name="sourceTimeZone"/>.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> value that represents the date and time in the destination time zone which 
        /// corresponds to the <paramref name="dateTime"/> parameter in the source time zone. 
        /// </returns>
        public static DateTime ChangeTimeZone(
            this DateTime dateTime,
            TimeZoneInfo sourceTimeZone,
            TimeZoneInfo destinationTimeZone,
            bool assumeLocal)
        {
            var utcTimeZone = TimeZoneInfo.Utc;
            var sourceIsUtc = utcTimeZone.Equals(sourceTimeZone);
            var destinationIsUtc = utcTimeZone.Equals(destinationTimeZone);
            if (sourceIsUtc && destinationIsUtc)
            {
                return new DateTime(dateTime.Ticks, DateTimeKind.Utc);
            }
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                if (!assumeLocal && !sourceIsUtc)
                {
                    dateTime = TimeZoneInfo.ConvertTime(dateTime, utcTimeZone, sourceTimeZone);
                }
            }
            else if (dateTime.Kind == DateTimeKind.Utc)
            {
                if (!sourceIsUtc)
                {
                    dateTime = TimeZoneInfo.ConvertTime(dateTime, utcTimeZone, sourceTimeZone);
                }
            }
            return new DateTime(
                TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone).Ticks,
                destinationIsUtc ? DateTimeKind.Utc : DateTimeKind.Unspecified);
        }
        /// <summary>
        /// Converts a time from one time zone to another. 
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> instance upon which this extension method is invoked.
        /// </param>
        /// <param name="sourceTimeZone">
        /// The time zone of the given <paramref name="dateTime"/>. 
        /// </param>
        /// <param name="destinationTimeZone">
        /// The time zone to convert <paramref name="dateTime"/> to.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> value that represents the date and time in the destination time zone which 
        /// corresponds to the <paramref name="dateTime"/> parameter in the source time zone. 
        /// </returns>
        public static DateTime ChangeTimeZone(
            this DateTime dateTime,
            TimeZoneInfo sourceTimeZone,
            TimeZoneInfo destinationTimeZone)
        {
            return ChangeTimeZone(dateTime, sourceTimeZone, destinationTimeZone, false);
        }

        /// <summary>
        /// Converts a time to the universal time zone (UTC).
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> instance upon which this extension method is invoked.
        /// </param>
        /// <param name="sourceTimeZone">
        /// The time zone of the given <paramref name="dateTime"/>. 
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> value that represents the date and time in the UTC time zone which 
        /// corresponds to the <paramref name="dateTime"/> parameter. 
        /// </returns>
        public static DateTime ToUniversalTime(this DateTime dateTime, TimeZoneInfo sourceTimeZone)
        {
            return ChangeTimeZone(dateTime, sourceTimeZone, TimeZoneInfo.Utc, true);
        }
    }
}