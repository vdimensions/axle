using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="char">character</see> to a valid <see cref="char"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class CharacterParser : AbstractParser<char>
    {
        /// <inheritdoc />
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        protected override char DoParse(string value, IFormatProvider formatProvider) => char.Parse(value);
        #elif NETSTANDARD1_0_OR_NEWER
        protected override char DoParse(string value, IFormatProvider formatProvider)
        {
            return char.TryParse(value, out char res)
                ? res
                : throw new ParseException($"Could not parse value {value} into a valid character. ");
        }
        #endif

        /// <summary>
        /// Converts the specified string representation of a logical value to its <see cref="char"/> equivalent.
        /// A <see cref="Boolean">boolean</see> return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to convert.
        /// </param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific 
        /// format recognition.</param>
        /// <param name="output">
        /// When this method returns, contains the <see cref="char"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <see cref="char"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c><see langword="null"/></c> or is not 
        /// of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if value was converted successfully;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public override bool TryParse(string value, IFormatProvider formatProvider, out char output) 
            => char.TryParse(value, out output);
    }
}
