using System;


namespace Axle.Conversion.Parsing
{
    [Serializable]
    partial class CharacterParser
    {
        protected override char DoParse(string value, IFormatProvider formatProvider) { return char.Parse(value); }
    }
}