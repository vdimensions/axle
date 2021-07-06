#if NETSTANDARD || NET20_OR_NEWER
using System.Globalization;


namespace Axle.Extensions.DateTimeOffset
{
    using DateTimeOffset = System.DateTimeOffset;

    /// <summary>
    /// A <see langword="static"/> class that contains extension methods for the 
    /// <see cref="DateTimeOffset"/> <see langword="struct"/>
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Converts the value of the current <paramref name="dateTimeOffset"/> to its ISO 8601 string representation.
        /// </summary>
        /// <param name="dateTimeOffset">
        /// The <see cref="DateTimeOffset"/> value to convert to <see cref="string"/>
        /// </param>
        /// <returns>
        /// Am ISO 8601 string representation of the current <paramref name="dateTimeOffset"/> value.
        /// </returns>
        public static string ToISOString(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFK", CultureInfo.InvariantCulture);
        }
    }
}
#endif