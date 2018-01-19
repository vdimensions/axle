using System;


namespace Axle.Conversion.Parsing
{
    partial class CharacterParser
    {
        protected override char DoParse(string value, IFormatProvider formatProvider) { return value[0]; }
    }
}