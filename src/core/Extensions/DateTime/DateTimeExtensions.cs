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
        /// For example, if month equals 2 for February, the return value is 28 or 29 depending upon whether year is a 
        /// leap year. 
        /// </returns>
        /// <seealso cref="DateTime.DaysInMonth" />
        public static int DaysInMonth(this DateTime current)
        {
            return DateTime.DaysInMonth(current.Year, current.Month);
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
            return new DateTime(current.Year, current.Month, 1, 0, 0, 0, current.Kind);
        }
        public static DateTime FirstDayOfYear(this DateTime current)
        {
            return new DateTime(current.Year, 1, 1, 0, 0, 0, current.Kind);
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
            return new DateTime(current.Year, current.Month, DaysInMonth(current), 0, 0, 0, 0, current.Kind);
        }

        public static DateTime LastDayOfYear(this DateTime current)
        {
            return new DateTime(current.Year, 12, 31, 0, 0, 0, current.Kind);
        }


        public static DateTime ChangeKind(this DateTime dateTime, DateTimeKind kind) { return new DateTime(dateTime.Ticks, kind); }

        public static DateTime ChangeKindToLocal(this DateTime dateTime) { return ChangeKind(dateTime, DateTimeKind.Local); }

        public static DateTime ChangeKindToUtc(this DateTime dateTime) { return ChangeKind(dateTime, DateTimeKind.Utc); }
    }
}