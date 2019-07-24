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
        private readonly NumberStyles _numberStyles;

        public DoubleParser() : this(NumberStyles.Any&~NumberStyles.AllowThousands) { }
        public DoubleParser(NumberStyles numberStyles)
        {
            _numberStyles = numberStyles;
        }

        /// <inheritdoc />
        protected override Double DoParse(string value, IFormatProvider formatProvider)
        {
            return Double.Parse(value, _numberStyles, formatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Double output)
        {
            return Double.TryParse(value, _numberStyles, formatProvider, out output);
        }
    }
}
