using System;

namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of an
    /// <see cref="Type">type</see> to a valid <see cref="Type"/> instance.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class TypeParser : AbstractParser<Type>
    {
        /// <inheritdoc />
        protected override Type DoParse(string value, IFormatProvider formatProvider)
        {
            return Type.GetType(value);
        }
    }
}