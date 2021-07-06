using System;

namespace Axle.Text.Parsing
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
        protected override Uri DoParse(CharSequence value, IFormatProvider formatProvider) 
            => new Uri(value.ToString(), UriKind.RelativeOrAbsolute);

        /// <inheritdoc />
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out Uri output) 
            => Uri.TryCreate(value.ToString(), UriKind.RelativeOrAbsolute, out output);
    }
}
