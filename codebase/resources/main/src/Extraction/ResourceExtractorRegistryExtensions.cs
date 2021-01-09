using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// Contains common extension methods to the <see cref="IResourceExtractorRegistry"/> interface.
    /// </summary>
    public static class ResourceExtractorRegistryExtensions
    {
        private static IResourceExtractorRegistry DoRegister(IResourceExtractorRegistry registry, IEnumerable<IResourceExtractor> extractors)
        {
            return registry.Register(CompositeResourceExtractor.Create(extractors));
        }

        /// <summary>
        /// Stores the provided <see cref="IResourceExtractor"/>.
        /// </summary>
        /// <param name="registry">
        /// The <see cref="IResourceExtractorRegistry"/> instance to register extractors with.
        /// </param>
        /// <param name="extractors">
        /// A collection of <see cref="IResourceExtractor"/> instances to be registered. 
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IResourceExtractorRegistry"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="registry"/> is <c>null</c>.
        /// </exception>
        public static IResourceExtractorRegistry Register(this IResourceExtractorRegistry registry, IEnumerable<IResourceExtractor> extractors)
        {
            return DoRegister(
                registry.VerifyArgument(nameof(registry)).IsNotNull().Value, 
                extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value);
        }

        /// <summary>
        /// Stores the provided <see cref="IResourceExtractor"/>.
        /// </summary>
        /// <param name="registry">
        /// The <see cref="IResourceExtractorRegistry"/> instance to register extractors with.
        /// </param>
        /// <param name="extractors">
        /// A collection of <see cref="IResourceExtractor"/> instances to be registered. 
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IResourceExtractorRegistry"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Either <paramref name="registry"/> or <paramref name="extractors"/> is <c>null</c>.
        /// </exception>
        public static IResourceExtractorRegistry Register(this IResourceExtractorRegistry registry, params IResourceExtractor[] extractors)
        {
            return DoRegister(
                registry.VerifyArgument(nameof(registry)).IsNotNull().Value,
                extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value);
        }
    }
}