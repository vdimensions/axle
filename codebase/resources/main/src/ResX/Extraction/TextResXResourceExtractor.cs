#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using System.Globalization;
using Axle.Extensions.Uri;

namespace Axle.Resources.ResX.Extraction
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting only text resources.
    /// </summary>
    internal sealed class TextResXResourceExtractor : AbstractResXResourceExtractor
    {
        protected override ResourceInfo ExtractResource(Uri location, CultureInfo culture, Type resxType, string name)
        {
            var lookupName = location.Resolve(name).ToString().TrimStart('/');
            var val = new System.Resources.ResourceManager(resxType).GetString(lookupName, culture);
            return val == null ? null : new TextResourceInfo(name, culture, val);
        }
    }
}
#endif