#if NETFRAMEWORK
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using Axle.Extensions.Uri;

namespace Axle.Resources.ResX.Extraction
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting all resource types capable of being stored in a .resx file.
    /// </summary>
    /// <remarks>
    /// This implementation depends on the <see cref="System.Drawing">System.Drawing</see> assembly in order to support 
    /// the <see cref="Image"/> and <see cref="Icon"/> objects.
    /// </remarks>
    internal sealed class CompleteResXResourceExtractor : AbstractResXResourceExtractor
    {
        /// <inheritdoc />
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
                    // This will avoid issues when the resource is latter being marshalled to another form.
                    return new ResXStreamResourceInfo(resolver, location, name, culture);
                case Image image:
                    return new ImageResourceInfo(name, culture, image);
                case Icon icon:
                    return new IconResourceInfo(name, culture, icon);
                default:
                    return null;
            }
        }
    }
}
#endif