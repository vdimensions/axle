using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A class representing the resource extraction chain for a <see cref="ResourceContext">resource context</see>.
    /// It allows to obtain other required resources up the chain while in the process of extracting a particular resource.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    public sealed class ContextExtractionChain
    {
        private readonly IEnumerable<ResourceContext> _subContexts;
        private readonly IResourceExtractor[] _extractors;

        internal ContextExtractionChain(IEnumerable<ResourceContext> subContexts, IResourceExtractor[] extractors)
        {
            _subContexts = subContexts;
            _extractors = extractors;
        }

        private IEnumerable<ResourceInfo> DoExtractAll(string name)
        {
            foreach (var subContext in _subContexts)
            {
                foreach (var extractor in _extractors)
                {
                    var resource = extractor.Extract(subContext, name);
                    if (resource == null)
                    {
                        continue;
                    }
                    resource.Bundle = subContext.Bundle;
                    yield return resource;
                }
                foreach (var resource in subContext.ExtractionChain.DoExtractAll(name))
                {
                    // The `Bundle` property must already be set in the chained extractor, no need to set it again here.
                    //
                    yield return resource;
                }
            }
        }

        /// <summary>
        /// Attempts to extract a resource from the current <see cref="ContextExtractionChain">extraction chain</see>
        /// that matches the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the resource to lookup.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the located resource if found; <c>null</c> otherwise.
        /// </returns>
        public ResourceInfo Extract(string name) => DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty()).FirstOrDefault();

        /// <summary>
        /// Attempts to extract all resources from the current <see cref="ContextExtractionChain">extraction chain</see>
        /// that match the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the resource to lookup.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ResourceInfo"/> instances representing resources that have successfully been extracted from 
        /// the current <see cref="ContextExtractionChain">chain</see>.
        /// </returns>
        public IEnumerable<ResourceInfo> ExtractAll(string name) => DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty());
    }
}
