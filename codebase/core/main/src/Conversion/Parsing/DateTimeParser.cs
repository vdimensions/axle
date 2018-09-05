#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="DateTime">time instant</see> to a valid <see cref="DateTime"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DateTimeParser : AbstractStrictParser<DateTime>
    {
        /// <inheritdoc />
        protected override DateTime DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? DateTime.Parse(value, formatProvider) : DateTime.Parse(value);
        }

        /// <inheritdoc />
        protected override DateTime DoParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return DateTime.ParseExact(value, format, formatProvider);
        }
    }
}
#endif