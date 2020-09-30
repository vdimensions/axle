#if NETSTANDARD || NET20_OR_NEWER
using System;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="bool">boolean</see> to a valid <see cref="bool"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class BooleanParser : AbstractParser<bool>
    {
        /// <inheritdoc />
        protected override bool DoParse(CharSequence value, IFormatProvider formatProvider) 
            => bool.Parse(value.ToString());

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <see cref="bool"/> equivalent.
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
        /// When this method returns, <paramref name="output"/> contains the <see cref="bool"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="bool"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c><see langword="null"/></c> or is not 
        /// of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if value was converted successfully; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out bool output) 
            => bool.TryParse(value.ToString(), out output);

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <see cref="bool"/> equivalent.
        /// A <see cref="Boolean">boolean</see> return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string">string</see> containing the value to convert.
        /// </param>
        /// <param name="output">
        /// When this method returns, <paramref name="output"/> contains the <see cref="bool"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="bool"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c><see langword="null"/></c> or is not 
        /// of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if value was converted successfully; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public override bool TryParse(CharSequence value, out bool output) 
            => bool.TryParse(value.ToString(), out output);
    }
}
#endif
