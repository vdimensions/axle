#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using System.Globalization;

using Axle.Extensions.Uri;
using Axle.References;


namespace Axle.Resources.Extraction.ResX
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting only text resources.
    /// </summary>
    internal sealed class TextResXResourceExtractor : AbstractResXResourceExtractor
    {
        protected override Nullsafe<ResourceInfo> ExtractResource(Uri location, CultureInfo culture, Type resxType, string name)
        {
            var lookupName = location.Resolve(name).ToString().TrimStart('/');
            var val = new System.Resources.ResourceManager(resxType).GetString(lookupName, culture);
            return val == null ? Nullsafe<ResourceInfo>.None : new TextResourceInfo(name, culture, val);
        }
    }
}
#endif