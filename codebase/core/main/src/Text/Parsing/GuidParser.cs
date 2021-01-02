using System;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a
    /// <see cref="Guid">globally unique identifier</see> to a valid <see cref="Guid"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class GuidParser : AbstractParser<Guid>
    {
        /// <inheritdoc />
        protected override Guid DoParse(CharSequence value, IFormatProvider formatProvider) => new Guid(value.ToString());
    }
}
