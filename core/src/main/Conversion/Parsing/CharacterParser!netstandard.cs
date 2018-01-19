using System;


namespace Axle.Conversion.Parsing
{
    [Serializable]
    partial class CharacterParser
    {
        /// <inheritdoc />
        protected override char DoParse(string value, IFormatProvider formatProvider) { return char.Parse(value); }
    }
}