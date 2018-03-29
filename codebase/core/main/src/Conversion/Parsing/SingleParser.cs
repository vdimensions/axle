using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="float">single precision floating point number</see> to a valid <see cref="float"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class SingleParser : AbstractParser<float>
    {
        private readonly CultureInfo _fallbackFormatProvider;

        public SingleParser() { }
        public SingleParser(CultureInfo fallbackFormatProvider)
        {
            _fallbackFormatProvider = fallbackFormatProvider;
        }

        /// <inheritdoc />
        protected override Single DoParse(string value, IFormatProvider formatProvider)
        {
            return Single.Parse(value, NumberStyles.Any, formatProvider ?? _fallbackFormatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Single output)
        {
            return Single.TryParse(value, NumberStyles.Any, formatProvider ?? _fallbackFormatProvider, out output);
        }
    }
}