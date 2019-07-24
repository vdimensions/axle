using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of  a
    /// <see cref="decimal">decimal</see> to a valid <see cref="decimal"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DecimalParser : AbstractParser<Decimal>
    {
        private readonly NumberStyles _numberStyles;

        public DecimalParser() : this(NumberStyles.Any&~NumberStyles.AllowThousands) { }
        public DecimalParser(NumberStyles numberStyles)
        {
            _numberStyles = numberStyles;
        }

        /// <inheritdoc />
        protected override Decimal DoParse(string value, IFormatProvider formatProvider)
        {
            return Decimal.Parse(value, _numberStyles, formatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Decimal output)
        {
            return Decimal.TryParse(value, _numberStyles, formatProvider, out output);
        }
    }
}
