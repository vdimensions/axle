using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="bool">boolean</see> to a valid <see cref="bool"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class BooleanParser : AbstractParser<bool>
    {
        protected override bool DoParse(string value, IFormatProvider formatProvider)
        {
            return bool.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out bool output)
        {
            return bool.TryParse(value, out output);
        }

        public override bool TryParse(string value, out bool output)
        {
            return bool.TryParse(value, out output);
        }
    }
}
