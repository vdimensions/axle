using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="int">32-bit integer</see> to a valid <see cref="int"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class Int32Parser : AbstractParser<int>
    {
        /// <inheritdoc />
        protected override Int32 DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Int32.Parse(value, formatProvider) : Int32.Parse(value);
        }

        /// <inheritdoc />
        public override bool TryParse(string value, IFormatProvider formatProvider, out Int32 output)
        {
            return formatProvider != null
                ? Int32.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : Int32.TryParse(value, out output);
        }
    }
}
