using System;

namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="DateTimeOffset">datetime offset</see> to a valid <see cref="DateTimeOffset"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DateTimeOffsetParser : AbstractStrictParser<DateTimeOffset>
    {
        /// <inheritdoc />
        protected override DateTimeOffset DoParse(string value, IFormatProvider formatProvider)
        {
            return DateTimeOffset.Parse(value, formatProvider);
        }

        /// <inheritdoc />
        protected override DateTimeOffset DoParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return DateTimeOffset.ParseExact(value, format, formatProvider);
        }
    }
}
