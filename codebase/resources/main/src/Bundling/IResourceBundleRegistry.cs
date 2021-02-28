using System.Collections.Generic;

namespace Axle.Resources.Bundling
{
    /// <summary>
    /// An interface for a resource bundle registry; that is, an object which is used
    /// to store instances of <see cref="IResourceBundleContent" /> to latter be used 
    /// by a <see cref="ResourceManager" /> implementation.
    /// </summary>
    public interface IResourceBundleRegistry : IEnumerable<IResourceBundleContent>
    {
        /// <summary>
        /// Exposes the <see cref="IResourceBundleContent"/> instance associated with the given <paramref name="bundle"/>,
        /// or creates a new one if not already existing.
        /// </summary>
        /// <param name="bundle">
        /// The name of the resource bundle.
        /// </param>
        /// <returns>
        /// A <see cref="IResourceBundleContent"/> instance associated with the given <paramref name="bundle"/>. 
        /// </returns>
        IConfigurableBundleContent Configure(string bundle);

        /// <summary>
        /// Gets an instance of <see cref="IResourceBundleContent"/> object that represent the resource lookup locations
        /// for a given resource <paramref name="bundle"/>.  
        /// </summary>
        /// <param name="bundle">
        /// The name of the resource bundle. 
        /// </param>
        /// <returns>
        /// An instance of <see cref="IResourceBundleContent"/> object that represent the resource lookup locations for
        /// the given resource <paramref name="bundle"/>, or an empty collection if the bundle
        /// has not been configured yet. 
        /// </returns>
        IResourceBundleContent this[string bundle] { get; }
    }
}