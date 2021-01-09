using System.Collections.Generic;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface for a resource extractor registry; that is, an object which is used
    /// to store instances of <see cref="T:Axle.Resources.Extraction.IResourceExtractor" /> to latter be used 
    /// by a <see cref="T:Axle.Resources.ResourceManager" /> implementation.
    /// </summary>
    public interface IResourceExtractorRegistry : IEnumerable<IResourceExtractor>
    {
        /// <summary>
        /// Stores the provided <see cref="IResourceExtractor"/>.
        /// </summary>
        /// <param name="extractor">
        /// The <see cref="IResourceExtractor"/> instance to be registered. 
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IResourceExtractorRegistry"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="extractor"/> is <c>null</c>.
        /// </exception>
        IResourceExtractorRegistry Register(IResourceExtractor extractor);
    }
}