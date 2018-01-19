using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="sbyte">signed byte</see> to a valid <see cref="sbyte"/> value.
    /// </summary>
    #if !netstandard
    [Serializable]
    #endif
    //[Stateless]
    public sealed class SByteParser : AbstractParser<sbyte>
    {
        /// <inheritdoc />
        protected override SByte DoParse(string value, IFormatProvider formatProvider)
        {
            return SByte.Parse(value, formatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out SByte output)
        {
            return SByte.TryParse(value, NumberStyles.None, formatProvider, out output);
        }
    }
}