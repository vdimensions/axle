using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a 
    /// <see cref="TimeSpan">time interval</see> to a valid <see cref="TimeSpan"/> value.
    /// </summary>
    //[Stateless]
    public sealed partial class TimeSpanParser
    {
        /// <inheritdoc />
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