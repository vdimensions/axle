using System;
using System.Collections.Generic;


namespace Axle.Resources.Bundling
{
    /// <summary>
    /// An interface facilitating the management of resource lookup locations
    /// for a given resource bundle. 
    /// </summary>
    public interface IResourceBundleContent : IEnumerable<Uri>
    {
        /// <summary>
        /// Registers the given <see cref="Uri"/> as a lookup location for the represented by the current 
        /// <see cref="IResourceBundleContent"/> instance bundle.
        /// </summary>
        /// <param name="location">
        /// The location to register.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IResourceBundleContent"/> instance. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="location"/> is <c>null</c>.
        /// </exception>
        IResourceBundleContent Register(Uri location);
    }
}