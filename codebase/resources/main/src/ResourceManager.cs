#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System.Globalization;
using System.Linq;

using Axle.Globalization.Extensions.CultureInfo;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// An abstract class to serve as a base for a resource manager; that is, an object 
    /// that allows convenient access at runtime for a wide range of resources, including 
    /// culture-specific data and embedded objects.
    /// </summary>
    public abstract class ResourceManager
    {
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation with the provided
        /// <paramref name="bundles"/> and <paramref name="extractors"/>.
        /// </summary>
        /// <param name="bundles">
        /// The <see cref="IResourceBundleRegistry"/> instance to be used by the current <see cref="ResourceManager"/> implementation.
        /// </param>
        /// <param name="extractors">
        /// The <see cref="IResourceExtractorRegistry"/> instance to be used by the current <see cref="ResourceManager"/> implementation.
        /// </param>
        protected ResourceManager(IResourceBundleRegistry bundles, IResourceExtractorRegistry extractors)
        {
            Bundles = bundles.VerifyArgument(nameof(bundles)).IsNotNull().Value;
            Extractors = extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value;
        }

        /// <summary>
        /// Attempts to resolve a resource object based on the provided parameters.
        /// </summary>
        /// <param name="bundle">
        /// The name of the resource bundle where the resource is contained.
        /// </param>
        /// <param name="name">
        /// The name of the resource to locate.
        /// </param>
        /// <param name="culture">
        /// A <see cref="CultureInfo"/> instance representing the culture for which the resource is being requested.
        /// </param>
        /// <returns></returns>
        public ResourceInfo Resolve(string bundle, string name, CultureInfo culture)
        {
            bundle.VerifyArgument(nameof(bundle)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            var bundles = Bundles[bundle].ToArray();
            var extractors = Extractors.ToArray();
            foreach (var ci in culture.ExpandHierarchy())
            {
                var context = new ResourceContext(bundle, bundles, ci, extractors);
                var result = context.ExtractionChain.Extract(name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Exposes the <see cref="IResourceBundleRegistry">resource bundle registry</see> that
        /// contains the list of locations which the current <see cref="ResourceManager"/> will use
        /// internally to resolve resources for a given bundle.
        /// </summary>
        public IResourceBundleRegistry Bundles { get; }

        /// <summary>
        /// Exposes the <see cref="IResourceExtractorRegistry">resource extractor registry</see> that
        /// contains the resource extractors which the current <see cref="ResourceManager"/> will use
        /// internally when resolving resources.
        /// </summary>
        public IResourceExtractorRegistry Extractors { get; }
    }
}
#endif