using System;
using System.Drawing;
using System.Globalization;
using System.IO;


namespace Axle.Resources.Native
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting all resource types capable of being stored in a .resx file.
    /// </summary>
    /// <remarks>
    /// This implementation depends on the <see cref="System.Drawing">System.Drawing</see> assembly in order to support 
    /// the <see cref="Image"/> and <see cref="Icon"/> objects.
    /// </remarks>
    public sealed class DefaultNativeResourceExtractor : AbstractNativeResourceExtractor
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultNativeResourceExtractor"/> class.
        /// </summary>
        /// <param name="type">
        /// The type that represents the .NET resource container.
        /// </param>
        public DefaultNativeResourceExtractor(Type type) : base(type)
        {
        }

        protected override ResourceInfo ExtractResource(NativeResourceResolver resolver, Uri resourceKey, CultureInfo culture)
        {
            switch (resolver.Resolve(resourceKey.ToString(), culture))
            {
                case string str:
                    return new StringResourceInfo(resourceKey, culture, str);
                case Stream stream:
                    // We do not need the actual stream here, we only used it to determine the resource type.
                    stream.Dispose();
                    // Create a resource representation that will always open a fresh stream when the underlying data is requested.
                    // This will avoid issues when the resource is latter being marshalled to another form.
                    var result = new NativeStreamResourceInfo(resolver, resourceKey, culture);
                    // return the resource
                    return result;
                case Image image:
                    return new ImageResourceInfo(resourceKey, culture, image);
                case Icon icon:
                    return new IconResourceInfo(resourceKey, culture, icon);
                default:
                    return null;
            }
        }
    }
}