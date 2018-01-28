using System;
using System.Globalization;
using System.Reflection;

using Axle.Verification;


namespace Axle.Resources.Native
{
    /// <summary>
    /// An class to serve as a base for <see cref="IResourceExtractor"/> implementations
    /// that handle the native .NET resource objects.
    /// </summary>
    /// <seealso cref="IResourceExtractor "/>
    public abstract class AbstractNativeResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;

        /// <summary>
        /// Creates a new instance of the current <see cref="AbstractNativeResourceExtractor"/> implementation.
        /// </summary>
        /// <param name="type">
        /// The type that represents the .NET resource container.
        /// </param>
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