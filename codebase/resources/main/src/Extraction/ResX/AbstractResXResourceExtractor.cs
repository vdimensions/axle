#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;

using Axle.Verification;


namespace Axle.Resources.Extraction.ResX
{
    /// <summary>
    /// An class to serve as a base for <see cref="IResourceExtractor"/> implementations
    /// that handle the native .NET resource objects.
    /// </summary>
    /// <seealso cref="IResourceExtractor "/>
    public abstract class AbstractResXResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;
        [Obsolete]
        protected readonly Uri BaseUri;

        /// <summary>
        /// Creates a new instance of the current <see cref="AbstractResXResourceExtractor"/> implementation.
        /// </summary>
        /// <param name="type">
        /// The type that represents the .NET resource container.
        /// </param>
        protected AbstractResXResourceExtractor(Type type)
        {
            _type = type.VerifyArgument(nameof(type)).IsNotNull();
        }

        /// <inheritdoc />
        public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            var resolver = new ResXResourceResolver(_type);

            return TryExtractResource(resolver, location, name, culture, out resource);
        }

        protected abstract bool TryExtractResource(ResXResourceResolver resolver, Uri location, string name, CultureInfo culture, out ResourceInfo resource);

        public System.Reflection.Assembly Assembly => _type.Assembly;
    }
}
#endif