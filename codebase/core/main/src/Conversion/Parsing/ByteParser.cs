using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of  a
    /// <see cref="byte">byte</see> to a valid <see cref="byte"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ByteParser : AbstractParser<byte>
    {
        /// <inheritdoc />
        protected override Byte DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Byte.Parse(value, formatProvider) : Byte.Parse(value);
        }

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <see cref="byte"/> equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string">string</see> containing the value to convert.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific format recognition.
        /// </param>
        /// <param name="output">
        /// When this method returns, <paramref name="output"/> contains the <see cref="byte"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="byte"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c>null</c> or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if value was converted successfully; otherwise, <c>false</c>.
        /// </returns>
        public override bool TryParse(string value, IFormatProvider formatProvider, out Byte output)
        {
            return formatProvider != null
                ? Byte.TryParse(value, NumberStyles.None, formatProvider, out output)
                : Byte.TryParse(value, out output);
        }
    }
}
