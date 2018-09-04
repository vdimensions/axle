using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a 
    /// <see cref="long">64-bit integer</see> to a valid <see cref="long"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class Int64Parser : AbstractParser<Int64>
    {
        /// <inheritdoc />
        protected override Int64 DoParse(string value, IFormatProvider formatProvider)
        {
            return formatProvider != null ? Int64.Parse(value, formatProvider) : Int64.Parse(value);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Int64 output)
        {
            return formatProvider != null
                ? Int64.TryParse(value, NumberStyles.Any, formatProvider, out output)
                : Int64.TryParse(value, out output);
        }
    }
}