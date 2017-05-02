using System;

using Axle.Conversion.Parsing;


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
        protected override char DoParse(string value, IFormatProvider formatProvider) { return char.Parse(value); }

        public override bool TryParse(string value, IFormatProvider formatProvider, out char output) { return char.TryParse(value, out output); }
    }
}