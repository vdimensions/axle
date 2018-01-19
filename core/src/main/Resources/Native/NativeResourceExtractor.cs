using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;

using Axle.Verification;


namespace Axle.Resources.Native
{
    public sealed class NativeResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;

        public NativeResourceExtractor(Type type)
        {
            _type = type.VerifyArgument(nameof(type)).IsNotNull();
        }

        /// <inheritdoc />
        public ResourceInfo Extract(Uri resourceKey, CultureInfo culture)
        {
            resourceKey.VerifyArgument(nameof(resourceKey)).IsNotNull();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            var resolver = new NativeResourceResolver(_type);
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

        public Assembly Assembly => _type.Assembly;
    }
}