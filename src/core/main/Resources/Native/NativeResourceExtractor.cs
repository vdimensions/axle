using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;

using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources.Native
{
    internal sealed class NativeResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;

        public NativeResourceExtractor(Type type)
        {
            _type = type.VerifyArgument(nameof(type)).IsNotNull();
        }

        public ResourceInfo Extract(Uri resourceKey, CultureInfo culture)
        {
            culture.VerifyArgument("culture").IsNotNull();
            var resolver = new NativeResourceResolver(_type);
            var rawResource = resolver.Resolve(resourceKey.ToString(), culture);
            if (rawResource == null)
            {
                return null;
            }
			if (rawResource is string str)
			{
				return new StringResourceInfo(resourceKey, culture, str);
			}
			if (rawResource is Stream stream)
			{
				// Create a resource representation that will always open a fresh stream when the underlying data is requested.
				// This will avoid issues when the resource is latter being marshalled to another form.
				var result = new NativeStreamResourceInfo(resolver, resourceKey, culture);
				// We do not need the actual stream here, we only used it to determine the resource type.
				stream.Dispose();
				// return the resource
				return result;
			}
			if (rawResource is Image image)
			{
				return new ImageResourceInfo(resourceKey, culture, image);
			}
			if (rawResource is Icon icon)
			{
				return new IconResourceInfo(resourceKey, culture, icon);
			}
            return null;
        }

        public Assembly Assembly => _type.Assembly;
    }
}