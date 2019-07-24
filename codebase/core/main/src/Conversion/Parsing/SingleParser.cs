using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of
    /// a <see cref="float">single precision floating point number</see> to a valid <see cref="float"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class SingleParser : AbstractParser<float>
    {
        private readonly NumberStyles _numberStyles;

        public SingleParser() : this(NumberStyles.Any&~NumberStyles.AllowThousands) { }
        public SingleParser(NumberStyles numberStyles)
        {
            _numberStyles = numberStyles;
        }

        /// <inheritdoc />
        protected override Single DoParse(string value, IFormatProvider formatProvider)
        {
            return Single.Parse(value, _numberStyles, formatProvider);
        }

        /// <inheritdoc />
        public override bool TryParse(string value, IFormatProvider formatProvider, out Single output)
        {
            return Single.TryParse(value, _numberStyles, formatProvider, out output);
        }
    }
}
