using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="TimeSpan">time interval</see> to a valid <see cref="TimeSpan"/> value.
    /// </summary>
    //[Stateless]
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    #if NETSTANDARD1_0_OR_NEWER || NET40_OR_NEWER
    public sealed class TimeSpanParser : AbstractStrictParser<TimeSpan>
    #else
    public sealed class TimeSpanParser : AbstractParser<TimeSpan>
    #endif
    {
        /// <inheritdoc />
        protected override TimeSpan DoParse(string value, IFormatProvider formatProvider)
        {
            return TimeSpan.Parse(value);
        }

        /// <inheritdoc />
        public override bool TryParse(string value, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParse(value, out output);
        }

        #if NETSTANDARD1_0_OR_NEWER || NET40_OR_NEWER
        /// <inheritdoc />
        protected override TimeSpan DoParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return TimeSpan.ParseExact(value, format, formatProvider, System.Globalization.TimeSpanStyles.None);
        }

        public override bool TryParseExact(string value, string format, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParseExact(value, format, formatProvider, System.Globalization.TimeSpanStyles.None, out output);
        }
        #endif
    }
}
