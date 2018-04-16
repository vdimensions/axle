using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="Guid">globally unique identifier</see> to a valid <see cref="Guid"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class GuidParser : AbstractParser<Guid>
    {
        /// <inheritdoc />
        protected override Guid DoParse(string value, IFormatProvider formatProvider) => new Guid(value);
    }
}