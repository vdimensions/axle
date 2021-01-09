using System;
using System.Threading.Tasks;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing a resource extractor; that is, an object responsible for locating raw resources before
    /// being unmarshalled to a suitable resource representation.
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
        /// A <see cref="ResourceInfo"/> instance representing the extracted resource, or <c>null</c> if the resource
        /// was not found. 
        /// </returns>
        /// <seealso cref="ExtractAsync"/>
        /// <seealso cref="ResourceContext"/>
        ResourceInfo Extract(IResourceContext context, string name);

        /// <summary>
        /// Attempts to asynchronously locate a resource based on the provided parameters.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ResourceContext"/> instance that represents the context
        /// of the current resource extraction.
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource.
        /// </param>
        /// <returns>
        /// A <see cref="Task{ResourceInfo}"/> instance representing the asynchronous operation for retrieving the
        /// resource. 
        /// </returns>
        /// <seealso cref="Extract"/>
        /// <seealso cref="ResourceContext"/>
        Task<ResourceInfo> ExtractAsync(IResourceContext context, string name);
    }
}