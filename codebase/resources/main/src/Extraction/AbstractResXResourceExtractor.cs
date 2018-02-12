#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An class to serve as a base for <see cref="IResourceExtractor"/> implementations
    /// that handle the native .NET resource objects.
    /// </summary>
    /// <seealso cref="IResourceExtractor "/>
    public abstract class AbstractResXResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;
        protected readonly Uri BaseUri;

        /// <summary>
        /// Creates a new instance of the current <see cref="AbstractResXResourceExtractor"/> implementation.
        /// </summary>
        /// <param name="type">
        /// The type that represents the .NET resource container.
        /// </param>
        protected AbstractResXResourceExtractor(Type type, Uri baseUri)
        {
            _type = type.VerifyArgument(nameof(type)).IsNotNull();
            BaseUri = baseUri.VerifyArgument(nameof(baseUri)).IsNotNull();
        }

        /// <inheritdoc />
        public ResourceInfo Extract(string name, CultureInfo culture)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            var resolver = new ResXResourceResolver(_type);

            return ExtractResource(resolver, name, culture);
        }

        protected abstract ResourceInfo ExtractResource(ResXResourceResolver resolver, string name, CultureInfo culture);

        
        public System.Reflection.Assembly Assembly => _type.Assembly;
    }
}
#endif