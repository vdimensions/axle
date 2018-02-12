#if !NETSTANDARD
using System;
using System.Drawing;
using System.Globalization;
using System.IO;

using Axle.Extensions.Uri;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting all resource types capable of being stored in a .resx file.
    /// </summary>
    /// <remarks>
    /// This implementation depends on the <see cref="System.Drawing">System.Drawing</see> assembly in order to support 
    /// the <see cref="Image"/> and <see cref="Icon"/> objects.
    /// </remarks>
    public sealed class DefaultResXResourceExtractor : AbstractResXResourceExtractor
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResXResourceExtractor"/> class.
        /// </summary>
        /// <param name="type">
        /// The type that represents the .NET resource container.
        /// </param>
        public DefaultResXResourceExtractor(Type type, Uri baseUri) : base(type, baseUri)
        {
        }

        protected override ResourceInfo ExtractResource(ResXResourceResolver resolver, string name, CultureInfo culture)
        {
            var location = BaseUri.Resolve($"./{name}");
            switch (resolver.Resolve(location, culture))
            {
                case string str:
                    return new TextResourceInfo(location, name, culture, str);
                case Stream stream:
                    // We do not need the actual stream here, we only used it to determine the resource type.
                    stream.Dispose();
                    // Create a resource representation that will always open a fresh stream when the underlying data is requested.
                    // This will avoid issues when the resource is latter being marshalled to another form.
                    var result = new ResXStreamResourceInfo(resolver, location, name, culture);
                    // return the resource
                    return result;
                case Image image:
                    return new ImageResourceInfo(location, name, culture, image);
                case Icon icon:
                    return new IconResourceInfo(location, name, culture, icon);
                default:
                    return null;
            }
        }
    }
}
#endif