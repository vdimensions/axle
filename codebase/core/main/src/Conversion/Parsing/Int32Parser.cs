using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="int">32-bit integer</see> to a valid <see cref="int"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class Int32Parser : AbstractParser<int>
    {
        /// <inheritdoc />
        protected override Int32 DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Int32.Parse(value, formatProvider) : Int32.Parse(value);
        }

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <see cref="int"/> equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string">string</see> containing the value to convert.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific format recognition.
        /// </param>
        /// <param name="output">
        /// When this method returns, <paramref name="output"/> contains the <see cref="int"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="int"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c>null</c> or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if value was converted successfully; otherwise, <c>false</c>.
        /// </returns>
        public override bool TryParse(string value, IFormatProvider formatProvider, out Int32 output)
        {
            return formatProvider != null
                ? Int32.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : Int32.TryParse(value, out output);
        }
    }
}
