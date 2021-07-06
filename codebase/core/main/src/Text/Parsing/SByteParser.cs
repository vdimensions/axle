using System;
using System.Globalization;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="sbyte">signed byte</see>
    /// to a valid <see cref="sbyte"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class SByteParser : AbstractParser<sbyte>
    {
        /// <inheritdoc />
        protected override SByte DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return SByte.Parse(value.ToString(), formatProvider);
        }

        /// <inheritdoc />
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out SByte output)
        {
            return SByte.TryParse(value.ToString(), NumberStyles.Any, formatProvider, out output);
        }
    }
}
