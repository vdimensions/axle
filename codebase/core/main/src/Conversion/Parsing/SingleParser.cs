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

        /// <summary>
        /// Creates a new instance of the <see cref="SingleParser"/> class.
        /// </summary>
        public SingleParser() : this(NumberStyles.Any&~NumberStyles.AllowThousands) { }
        /// <summary>
        /// Creates a new instance of the <see cref="SingleParser"/> class using the provided 
        /// <paramref name="numberStyles"/>.
        /// </summary>
        /// <param name="numberStyles">
        /// One of the <see cref="NumberStyles"/> enumeration values.
        /// </param>
        public SingleParser(NumberStyles numberStyles)
        {
            _numberStyles = numberStyles;
        }

        /// <inheritdoc />
        protected override Single DoParse(string value, IFormatProvider formatProvider)
        {
            return Single.Parse(value, _numberStyles, formatProvider);
        }

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <see cref="float"/> equivalent.
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
        /// When this method returns, <paramref name="output"/> contains the <see cref="float"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="float"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c><see langword="null"/></c> or is not 
        /// of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if value was converted successfully; otherwise, <c>false</c>.
        /// </returns>
        public override bool TryParse(string value, IFormatProvider formatProvider, out Single output)
        {
            return Single.TryParse(value, _numberStyles, formatProvider, out output);
        }
    }
}
