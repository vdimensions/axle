using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="float">single precision floating point number</see> to a valid <see cref="float"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class SingleParser : AbstractParser<float>
    {
        protected override Single DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Single.Parse(value, formatProvider) : Single.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Single output)
        {
            return formatProvider != null
                ? Single.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : Single.TryParse(value, out output);
        }
    }
}