using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="decimal">decimal</see> to a valid <see cref="decimal"/> value.
    /// </summary>
    #if !netstandard
    [Serializable]
    #endif
    //[Stateless]
    public sealed class DecimalParser : AbstractParser<Decimal>
    {
        protected override Decimal DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Decimal.Parse(value, formatProvider) : Decimal.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Decimal output)
        {
            return formatProvider != null
                ? Decimal.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : Decimal.TryParse(value, out output);
        }
    }
}
