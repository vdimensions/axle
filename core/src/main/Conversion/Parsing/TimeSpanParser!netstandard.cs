using System;


namespace Axle.Conversion.Parsing
{
    [Serializable]
#if net40
    partial class TimeSpanParser : AbstractStrictParser<TimeSpan>
    {
        /// <inheritdoc />
        protected override TimeSpan DoParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return TimeSpan.ParseExact(value, format, formatProvider, System.Globalization.TimeSpanStyles.None);
        }

        /// <inheritdoc />
        public override bool TryParseExact(string value, string format, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParseExact(value, format, formatProvider, System.Globalization.TimeSpanStyles.None, out output);
        }
    }
#else
    partial class TimeSpanParser : AbstractParser<TimeSpan> { }
#endif
}