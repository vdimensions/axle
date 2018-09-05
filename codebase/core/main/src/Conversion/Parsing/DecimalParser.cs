#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="decimal">decimal</see> to a valid <see cref="decimal"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DecimalParser : AbstractParser<Decimal>
    {
        private readonly CultureInfo _fallbackFormatProvider;

        public DecimalParser() { }
        public DecimalParser(CultureInfo fallbackFormatProvider)
        {
            _fallbackFormatProvider = fallbackFormatProvider;
        }

        /// <inheritdoc />
        protected override Decimal DoParse(string value, IFormatProvider formatProvider)
        {
            return Decimal.Parse(value, NumberStyles.Any, formatProvider ?? _fallbackFormatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Decimal output)
        {
            return Decimal.TryParse(value, NumberStyles.Any, formatProvider ?? _fallbackFormatProvider, out output);
        }
    }
}
#endif
