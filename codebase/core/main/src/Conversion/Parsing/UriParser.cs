#if NETSTANDARD || NET35_OR_NEWER
using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// an <see cref="Uri">uniform resource identifier</see> to a valid <see cref="Uri"/> instance.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class UriParser : AbstractParser<Uri>
    {
        /// <inheritdoc />
        protected override Uri DoParse(string value, IFormatProvider formatProvider) => new Uri(value, UriKind.RelativeOrAbsolute);

        public override bool TryParse(string value, IFormatProvider formatProvider, out Uri output)
        {
            return Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out output);
        }
    }
}
#endif