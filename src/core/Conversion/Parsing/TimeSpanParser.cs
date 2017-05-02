using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="TimeSpan">time interval</see> to a valid <see cref="TimeSpan"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class TimeSpanParser : AbstractParser<TimeSpan>
    {
        protected override TimeSpan DoParse(string value, IFormatProvider formatProvider)
        {
            return TimeSpan.Parse(value);
        }
        public override bool TryParse(string value, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParse(value, out output);
        }
    }
}