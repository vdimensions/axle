using System;
using System.Globalization;
using System.Reflection;

using Axle.Verification;


namespace Axle.Resources.Native
{
    public abstract class AbstractNativeResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;

        protected AbstractNativeResourceExtractor(Type type)
        {
            _type = type.VerifyArgument(nameof(type)).IsNotNull();
        }

        /// <inheritdoc />
        public ResourceInfo Extract(Uri resourceKey, CultureInfo culture)
        {
            resourceKey.VerifyArgument(nameof(resourceKey)).IsNotNull();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            var resolver = new NativeResourceResolver(_type);

            return ExtractResource(resolver, resourceKey, culture);
        }

        protected abstract ResourceInfo ExtractResource(NativeResourceResolver resolver, Uri resourceKey, CultureInfo culture);

        public Assembly Assembly => _type.Assembly;
    }
}