using System;
using System.Globalization;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of
    /// an <see cref="ushort">unsigned 16-bit integer</see> to a valid <see cref="ushort"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class UInt16Parser : AbstractParser<ushort>
    {
        /// <inheritdoc />
        protected override UInt16 DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return formatProvider != null 
                ? UInt16.Parse(value.ToString(), formatProvider) 
                : UInt16.Parse(value.ToString());
        }

        /// <inheritdoc />
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out UInt16 output)
        {
            return formatProvider != null
                ? UInt16.TryParse(value.ToString(), NumberStyles.Any, formatProvider, out output)
                : UInt16.TryParse(value.ToString(), out output);
        }
    }
}
