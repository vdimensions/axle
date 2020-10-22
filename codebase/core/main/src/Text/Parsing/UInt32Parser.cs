using System;
using System.Globalization;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// an <see cref="uint">unsigned 32-bit integer</see> to a valid <see cref="uint"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class UInt32Parser : AbstractParser<uint>
    {
        /// <inheritdoc />
        protected override UInt32 DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return formatProvider != null 
                ? UInt32.Parse(value.ToString(), formatProvider) 
                : UInt32.Parse(value.ToString());
        }

        /// <inheritdoc />
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out UInt32 output)
        {
            return formatProvider != null
                ? UInt32.TryParse(value.ToString(), NumberStyles.Any, formatProvider, out output)
                : UInt32.TryParse(value.ToString(), out output);
        }
    }
}
