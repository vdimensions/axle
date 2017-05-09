using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="char">character</see> to a valid <see cref="char"/> value.
    /// </summary>
    #if !netstandard
    [Serializable]
    #endif
    //[Stateless]
    public sealed class CharacterParser : AbstractParser<char>
    {
#if !netstandard
        protected override char DoParse(string value, IFormatProvider formatProvider) { return char.Parse(value); }
#else
        protected override char DoParse(string value, IFormatProvider formatProvider) { return value[0]; }
#endif

        public override bool TryParse(string value, IFormatProvider formatProvider, out char output) { return char.TryParse(value, out output); }
    }
}