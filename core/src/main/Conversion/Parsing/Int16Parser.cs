using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="short">16-bit integer</see> to a valid <see cref="short"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class Int16Parser : AbstractParser<short>
    {
        protected override Int16 DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Int16.Parse(value, formatProvider) : Int16.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Int16 output)
        {
            return formatProvider != null
                ? Int16.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : Int16.TryParse(value, out output);
        }
    }
}