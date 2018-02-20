namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing a resource extractor; that is, an object responsible for locating raw resources before being unmarshalled.
    /// </summary>
    public interface IResourceExtractor
    {
        /// <summary>
        /// Attempts to locate a resource based on the provided parameters.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ResourceContext"/> instance that represents the context
        /// of the current resource extraction.
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ResourceInfo"/> representing the extracted resource.
        /// </returns>
        /// <seealso cref="ResourceContext"/>
        ResourceInfo Extract(ResourceContext context, string name);
    }
}