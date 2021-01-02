using System;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="DateTimeOffset">datetime offset</see> to a valid <see cref="DateTimeOffset"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class DateTimeOffsetParser : AbstractStrictParser<DateTimeOffset>
    {
        /// <inheritdoc />
        protected override DateTimeOffset DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return DateTimeOffset.Parse(value.ToString(), formatProvider);
        }

        /// <inheritdoc />
        protected override DateTimeOffset DoParseExact(CharSequence value, string format, IFormatProvider formatProvider)
        {
            return DateTimeOffset.ParseExact(value.ToString(), format, formatProvider);
        }
    }
}
