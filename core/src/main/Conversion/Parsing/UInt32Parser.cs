using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// an <see cref="uint">unsigned 32-bit integer</see> to a valid <see cref="uint"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class UInt32Parser : AbstractParser<uint>
    {
        /// <inheritdoc />
        protected override UInt32 DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? UInt32.Parse(value, formatProvider) : UInt32.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out UInt32 output)
        {
            return formatProvider != null
                ? UInt32.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : UInt32.TryParse(value, out output);
        }
    }
}