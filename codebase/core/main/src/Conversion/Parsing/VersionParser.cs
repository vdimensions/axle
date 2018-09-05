#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="Version">version number</see> to a valid <see cref="Version"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class VersionParser : AbstractParser<Version>
    {
        /// <inheritdoc />
        protected override Version DoParse(string value, IFormatProvider formatProvider)
        {
            // TOOD: Specify version format provider

            return new Version(value);
        }
    }
}
#endif