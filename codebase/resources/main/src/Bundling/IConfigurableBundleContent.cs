using System;
using Axle.Resources.Extraction;


namespace Axle.Resources.Bundling
{
    /// <summary>
    /// An interface facilitating the management of resource lookup locations
    /// and bundle-specific resource extractors for a given resource bundle. 
    /// </summary>
    public interface IConfigurableBundleContent : IResourceBundleContent
    {
        /// <summary>
        /// Registers the given <see cref="Uri"/> as a lookup location for the represented by the current 
        /// <see cref="IConfigurableBundleContent"/> instance bundle.
        /// </summary>
        /// <param name="location">
        /// The location to register.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IConfigurableBundleContent"/> instance. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="location"/> is <c>null</c>.
        /// </exception>
        IConfigurableBundleContent Register(Uri location);

        /// <summary>
        /// Gets a reference to the <see cref="IResourceExtractorRegistry"/> that maintains the resource extractors
        /// exclusively available for the resource bundle that is currently configured.
        /// </summary>
        new IResourceExtractorRegistry Extractors { get; }
    }
}