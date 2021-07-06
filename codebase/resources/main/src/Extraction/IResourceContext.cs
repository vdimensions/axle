using System;
using System.Collections.Generic;
using System.Globalization;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing the context for resource lookup and extraction, including the
    /// <see cref="Location">location</see> where the resource should be looked up, the
    /// <see cref="Culture">culture</see> for which the resource has been requested, and methods for querying up other
    /// resources that would act as components for producing the requested resource.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    /// <seealso cref="ResourceManager"/>
    public interface IResourceContext
    {
        /// <summary>
        /// Attempts to extract a resource from the current <see cref="IResourceContext">resource context</see>
        /// that matches the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the resource to lookup.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the located resource if found; <c>null</c> otherwise.
        /// </returns>
        ResourceInfo Extract(string name);

        /// <summary>
        /// Attempts to extract all resources from the current
        /// <see cref="ResourceExtractionChain">extraction chain</see> that match the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the resource to lookup.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ResourceInfo"/> instances representing resources that have successfully been
        /// extracted from the current <see cref="IResourceContext">resource context</see>.
        /// </returns>
        IEnumerable<ResourceInfo> ExtractAll(string name);

        /// <summary>
        /// Gets the name of the resource bundle associated with this context.
        /// </summary>
        string Bundle { get; }

        /// <summary>
        /// Gets the <see cref="Uri"/> for the resource lookup location associated with this context.
        /// </summary>
        Uri Location { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> representing the culture  associated with this context.
        /// </summary>
        CultureInfo Culture { get; }
        
        /// <summary>
        /// Gets the <see cref="IResourceExtractor">resource extractor</see> associated with this context.
        /// </summary>
        IResourceExtractor Extractor { get; }
        
        /// <summary>
        /// Gets a reference to the next-in-chain <see cref="IResourceContext"/> instance, or <c>null</c> if this is
        /// the last resource context in the chain.
        /// </summary>
        IResourceContext Next { get; }
    }
}