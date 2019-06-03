using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="double">double precision floating point number</see> to a valid <see cref="double"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DoubleParser : AbstractParser<double>
    {
        private readonly CultureInfo _fallbackFormatProvider;

        public DoubleParser() { }
        public DoubleParser(CultureInfo fallbackFormatProvider)
        {
            _fallbackFormatProvider = fallbackFormatProvider;
        }

        /// <inheritdoc />
        protected override Double DoParse(string value, IFormatProvider formatProvider)
        {
            return Double.Parse(value, NumberStyles.Any, formatProvider ?? _fallbackFormatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Double output)
        {
            return Double.TryParse(value, NumberStyles.Any, formatProvider ?? _fallbackFormatProvider, out output);
        }
    }
}
