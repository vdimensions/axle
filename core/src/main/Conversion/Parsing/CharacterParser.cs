using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="char">character</see> to a valid <see cref="char"/> value.
    /// </summary>
    //[Stateless]
    public sealed partial class CharacterParser : AbstractParser<char>
    {
        public override bool TryParse(string value, IFormatProvider formatProvider, out char output) { return char.TryParse(value, out output); }
    }
}