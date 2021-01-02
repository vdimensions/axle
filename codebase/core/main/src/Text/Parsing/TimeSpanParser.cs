using System;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="TimeSpan">time interval</see> to a valid <see cref="TimeSpan"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    #if NETSTANDARD1_0_OR_NEWER || NET40_OR_NEWER || UNITY_2018_1_OR_NEWER
    public sealed class TimeSpanParser : AbstractStrictParser<TimeSpan>
    #else
    public sealed class TimeSpanParser : AbstractParser<TimeSpan>
    #endif
    {
        /// <inheritdoc />
        protected override TimeSpan DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return TimeSpan.Parse(value.ToString());
        }

        /// <inheritdoc />
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParse(value.ToString(), out output);
        }

        #if NETSTANDARD1_0_OR_NEWER || NET40_OR_NEWER || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        protected override TimeSpan DoParseExact(CharSequence value, string format, IFormatProvider formatProvider)
        {
            return TimeSpan.ParseExact(
                value.ToString(), format, formatProvider, System.Globalization.TimeSpanStyles.None);
        }

        /// <inheritdoc />
        public override bool TryParseExact(
            CharSequence value, string format, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParseExact(
                value.ToString(), format, formatProvider, System.Globalization.TimeSpanStyles.None, out output);
        }
        #endif
    }
}
