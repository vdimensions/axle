#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Axle.Extensions.Uri;

namespace Axle.Resources.ResX.Extraction
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting only strings and streamed resources.
    /// </summary>
    /// <remarks>
    /// This implementation does not depend on the <see cref="System.Drawing">System.Drawing</see> assembly.
    /// </remarks>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal sealed class SimpleResXResourceExtractor : AbstractResXResourceExtractor
    {
        protected override ResourceInfo ExtractResource(Uri location, CultureInfo culture, Type resxType, string name)
        {
            location = location.Resolve(name);
            var resolver = new ResXResourceResolver(resxType);
            switch (resolver.Resolve(location, culture))
            {
                case string str:
                    return new TextResourceInfo(name, culture, str);
                case Stream stream:
                    // We do not need the actual stream here, we only used it to determine the resource type.
                    stream.Dispose();
                    // Create a resource representation that will always open a fresh stream when the underlying data is requested.
                    // This will avoid issues when the resource is latter being marshaled to another form.
                    return new ResXStreamResourceInfo(resolver, location, name, culture);
                default:
                    return null;
            }
        }
    }
}
#endif