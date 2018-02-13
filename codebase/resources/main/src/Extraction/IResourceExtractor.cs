﻿namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing a resource extractor; that is, an object responsible for locating raw resources before being unmarshalled.
    /// </summary>
    /// <seealso cref="Axle.Resources.Marshalling.IResourceMarshaller"/>
    public interface IResourceExtractor
    {
        /// <summary>
        /// Attempts to locate a raw resource based on the provided parameters.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ResourceExtractionContext"/> instance that represents the context
        /// of the current resource extraction.
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ResourceInfo"/> representing the extracted resource.
        /// </returns>
        /// <seealso cref="ResourceExtractionContext"/>
        ResourceInfo Extract(ResourceExtractionContext context, string name);
    }
}