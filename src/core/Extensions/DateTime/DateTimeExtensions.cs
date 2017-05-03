using System;
using System.Globalization;


namespace Axle.Extensions.DateTime
{
    using DateTime = System.DateTime;

    /// <summary>
    /// A static class that contains extension methods for the <see cref="DateTime"/> struct
    /// </summary>
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// Returns the number of days in the specified month and year.
        /// </summary>
        /// <param name="current">The <see cref="DateTime"/> instance upon which this extension method is invoked.</param>
        /// <returns>
        /// Returns the number of days in the specified date's month and year. 
        /// For example, if month equals 2 for February, the return value is 28 or 29 depending upon whether year is a leap year. 
        /// </returns>
        /// <seealso cref="DateTime.DaysInMonth" />
        public static int DaysInMonth(this DateTime current)
        {
            return System.DateTime.DaysInMonth(current.Year, current.Month);
        }

        public static DateTime FirstDayOfWeek(this DateTime current, CultureInfo culture)
        {
            return FirstDayOfWeek(current, culture.DateTimeFormat.FirstDayOfWeek);
        }
        public static DateTime FirstDayOfWeek(this DateTime current, DayOfWeek firstDayOfWeek)
        {
            var currentDayOfWeek = current.DayOfWeek;

            if (firstDayOfWeek == currentDayOfWeek)
            {
                return current.Date;
            }

            var result = current.Date;
            for (var i = 1; i <= 7; i++)
            {
                result = result.AddDays(-1);
                if (result.DayOfWeek == firstDayOfWeek)
                {
                    break;
                }
            }
            return result;
        }
        public static DateTime FirstDayOfMonth(this DateTime current)
        {
            return new System.DateTime(current.Year, current.Month, 1, 0, 0, 0, current.Kind);
        }
        public static DateTime FirstDayOfYear(this DateTime current)
        {
            return new System.DateTime(current.Year, 1, 1, 0, 0, 0, current.Kind);
        }

        public static DateTime LastDayOfWeek(this DateTime current, CultureInfo culture)
        {
            return LastDayOfWeek(current, culture.DateTimeFormat.FirstDayOfWeek);
        }
        public static DateTime LastDayOfWeek(this DateTime current, DayOfWeek firstDayOfWeek)
        {
            return FirstDayOfWeek(current, firstDayOfWeek).AddDays(6);
        }

        public static DateTime LastDayOfMonth(this DateTime current)
        {
            return new System.DateTime(current.Year, current.Month, DaysInMonth(current), 0, 0, 0, 0, current.Kind);
        }

        public static DateTime LastDayOfYear(this DateTime current)
        {
            return new System.DateTime(current.Year, 12, 31, 0, 0, 0, current.Kind);
        }

        public static DateTime ToLocalTime(this DateTime current, DateTimeKind assumedKind)
        {
            var kind = current.Kind;
            return (kind == DateTimeKind.Unspecified ? assumedKind : kind) == DateTimeKind.Utc 
                ? TimeZoneInfo.ConvertTime(current, TimeZoneInfo.Utc, TimeZoneInfo.Local) 
                : new System.DateTime(current.Ticks, DateTimeKind.Local);
        }
        public static DateTime ToLocalTime(this DateTime current) { return ToLocalTime(current, DateTimeKind.Local); }

        /// <summary>
        /// Converts a time from one time zone to another. 
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance upon which this extension method is invoked.</param>
        /// <param name="sourceTimeZone">The time zone of <paramref name="dateTime"/>. </param>
        /// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime"/> to.</param>
        /// <param name="assumeLocal">
        /// A boolean value indicating whether the provided <see cref="DateTime"/> value in the
        /// <paramref name="dateTime"/> should be treated as a local time in the <paramref name="sourceTimeZone"/>
        /// if its <see cref="DateTime.Kind" /> property is set to <see cref="DateTimeKind.Unspecified"/>.
        /// If this value is <c>false</c>, the <paramref name="dateTime"/> is assumed to be an UTC date, otherwise
        /// it is treated as a date local to the timezone specified by the <paramref name="sourceTimeZone"/>.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> value that represents the date and time in the destination time zone that 
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
                return new System.DateTime(dateTime.Ticks, DateTimeKind.Utc);
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
            return new System.DateTime(
                TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone).Ticks,
                destinationIsUtc ? DateTimeKind.Utc : DateTimeKind.Unspecified);
        }
        public static DateTime ChangeTimeZone(
            this DateTime dateTime,
            TimeZoneInfo sourceTimeZone,
            TimeZoneInfo destinationTimeZone)
        {
            return ChangeTimeZone(dateTime, sourceTimeZone, destinationTimeZone, false);
        }

        public static DateTime ChangeKind(this DateTime dateTime, DateTimeKind kind) { return new System.DateTime(dateTime.Ticks, kind); }

        public static DateTime ChangeKindToLocal(this DateTime dateTime) { return ChangeKind(dateTime, DateTimeKind.Local); }

        public static DateTime ChangeKindToUtc(this DateTime dateTime) { return ChangeKind(dateTime, DateTimeKind.Utc); }
    
        public static DateTime ToUniversalTime(this DateTime dateTime, TimeZoneInfo sourceTimeZone)
        {
            return ChangeTimeZone(dateTime, sourceTimeZone, TimeZoneInfo.Utc, true);
        }
    }
}