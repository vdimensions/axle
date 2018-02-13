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
    public abstract class AbstractResXResourceExtractor : AbstractResourceExtractor
    {
        private readonly Type _type;

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
        protected sealed override ResourceInfo Extract(Uri location, CultureInfo culture, string name)
        {
            return ExtractResource(location, culture, new ResXResourceResolver(_type), name.VerifyArgument(nameof(name)).IsNotNullOrEmpty());
        }

        protected abstract ResourceInfo ExtractResource(Uri location, CultureInfo culture, ResXResourceResolver resolver, string name);

        public System.Reflection.Assembly Assembly => _type.Assembly;
    }
}
#endif