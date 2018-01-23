using System;
using System.Globalization;
using System.IO;


namespace Axle.Resources.Native
{
    /// <summary>
    /// The .NET's native resource extractor implementation, only strings and streamed resources.
    /// </summary>
    /// <remarks>
    /// This implementation does not depend on the <see cref="System.Drawing">System.Drawing</see> assembly.
    /// </remarks>
    public sealed class SimpleNativeResourceExtractor : AbstractNativeResourceExtractor
    {
        public SimpleNativeResourceExtractor(Type type) : base(type)
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
                default:
                    return null;
            }
        }
    }
}