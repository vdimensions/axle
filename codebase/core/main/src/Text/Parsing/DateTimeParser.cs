using System;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="DateTime">time instant</see> to a valid <see cref="DateTime"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class DateTimeParser : AbstractStrictParser<DateTime>
    {
        /// <inheritdoc />
        protected override DateTime DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? DateTime.Parse(value.ToString(), formatProvider) : DateTime.Parse(value.ToString());
        }

        /// <inheritdoc />
        protected override DateTime DoParseExact(CharSequence value, string format, IFormatProvider formatProvider)
        {
            return DateTime.ParseExact(value.ToString(), format, formatProvider);
        }
    }
}
