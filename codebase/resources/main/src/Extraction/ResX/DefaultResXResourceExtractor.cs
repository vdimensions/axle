#if !NETSTANDARD
using System;
using System.Drawing;
using System.Globalization;
using System.IO;

using Axle.Extensions.Uri;


namespace Axle.Resources.Extraction.ResX
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
        public DefaultResXResourceExtractor(Type type) : base(type)
        {
        }

        protected override bool TryExtractResource(ResXResourceResolver resolver, Uri location, string name, CultureInfo culture, out ResourceInfo resource)
        {
            location = location.Resolve($"./{name}");
            switch (resolver.Resolve(location, culture))
            {
                case string str:
                    resource = new TextResourceInfo(location, name, culture, str);
                    return true;
                case Stream stream:
                    // We do not need the actual stream here, we only used it to determine the resource type.
                    stream.Dispose();
                    // Create a resource representation that will always open a fresh stream when the underlying data is requested.
                    // This will avoid issues when the resource is latter being marshalled to another form.
                    resource = new ResXStreamResourceInfo(resolver, location, name, culture);
                    // return the resource
                    return true;
                case Image image:
                    resource = new ImageResourceInfo(location, name, culture, image);
                    return true;
                case Icon icon:
                    resource = new IconResourceInfo(location, name, culture, icon);
                    return true;
                default:
                    resource = null;
                    return false;
            }
        }
    }
}
#endif