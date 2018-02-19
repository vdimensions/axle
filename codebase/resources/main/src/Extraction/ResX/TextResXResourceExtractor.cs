#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System;
using System.Globalization;

using Axle.Extensions.Uri;


namespace Axle.Resources.Extraction.ResX
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting only text resources.
    /// </summary>
    public sealed class TextResXResourceExtractor : AbstractResXResourceExtractor
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TextResXResourceExtractor"/> class.
        /// </summary>
        public TextResXResourceExtractor() { }

        protected override ResourceInfo ExtractResource(Uri location, CultureInfo culture, Type resxType, string name)
        {
            var lookupName = location.Resolve(name).ToString().TrimStart('/');
            var val = new System.Resources.ResourceManager(resxType).GetString(lookupName, culture);
            return val == null ? null : new TextResourceInfo(name, culture, val);
        }
    }
}
#endif