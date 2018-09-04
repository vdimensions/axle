using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="byte">byte</see> to a valid <see cref="byte"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ByteParser : AbstractParser<byte>
    {
        /// <inheritdoc />
        protected override Byte DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Byte.Parse(value, formatProvider) : Byte.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Byte output)
        {
            return formatProvider != null
                ? Byte.TryParse(value, NumberStyles.None, formatProvider, out output)
                : Byte.TryParse(value, out output);
        }
    }
}