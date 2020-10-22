using System;
using System.Globalization;

namespace Axle.Text.Parsing
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

        /// <summary>
        /// Creates a new instance of the <see cref="DecimalParser"/> class.
        /// </summary>
        public DecimalParser() : this(NumberStyles.Any&~NumberStyles.AllowThousands) { }
        /// <summary>
        /// Creates a new instance of the <see cref="DecimalParser"/> class using the provided 
        /// <paramref name="numberStyles"/>.
        /// </summary>
        /// <param name="numberStyles">
        /// One of the <see cref="NumberStyles"/> enumeration values.
        /// </param>
        public DecimalParser(NumberStyles numberStyles)
        {
            _numberStyles = numberStyles;
        }

        /// <inheritdoc />
        protected override Decimal DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return Decimal.Parse(value.ToString(), _numberStyles, formatProvider);
        }

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <see cref="decimal"/> equivalent.
        /// A <see cref="Boolean">boolean</see> return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string">string</see> containing the value to convert.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific
        /// format recognition.
        /// </param>
        /// <param name="output">
        /// When this method returns, <paramref name="output"/> contains the <see cref="decimal"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="decimal"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c><see langword="null"/></c> or is not 
        /// of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if value was converted successfully;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out Decimal output)
        {
            return Decimal.TryParse(value.ToString(), _numberStyles, formatProvider, out output);
        }
    }
}
