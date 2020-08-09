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
    public sealed class ResourceExtractionChain
    {
        private readonly ResourceContext _ownerContext;
        private readonly IEnumerable<ResourceContext> _subContexts;
        internal readonly IEnumerable<IResourceExtractor> Extractors;

        internal ResourceExtractionChain(
            ResourceContext ownerContext, 
            IEnumerable<ResourceContext> subContexts, 
            IEnumerable<IResourceExtractor> extractors)
        {
            _ownerContext = ownerContext;
            _subContexts = subContexts;
            Extractors = extractors;
        }

        internal IEnumerable<ResourceInfo> DoExtractAll(string name)
        {
            var extractorContext = _ownerContext;
            foreach (var extractor in Extractors)
            {
                extractorContext = extractorContext.MoveOneExtractorForward();
                var resource = extractor.Extract(extractorContext, name);
                if (resource == null)
                {
                    // System.Console.WriteLine(
                    //     "Extracting resource [{0}] {1}::{2} {3} for location {4}.", 
                    //     _ownerContext.Culture.Name, 
                    //     _ownerContext.Bundle, 
                    //     name, 
                    //     resource == null ? "failed" : "succeeded",
                    //     _ownerContext.Location
                    // );
                    continue;
                }
                resource.Bundle = _ownerContext.Bundle;
                yield return resource;
            }
            foreach (var subContext in _subContexts)
            {
                foreach (var resource in subContext.ExtractionChain.DoExtractAll(name))
                {
                    yield return resource;
                }
            }
        }

        /// <summary>
        /// Attempts to extract a resource from the current <see cref="ResourceExtractionChain">extraction chain</see>
        /// that matches the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the resource to lookup.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the located resource if found; <c>null</c> otherwise.
        /// </returns>
        public ResourceInfo Extract(string name) => 
            DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty()).FirstOrDefault(x => x != null);

        /// <summary>
        /// Attempts to extract all resources from the current <see cref="ResourceExtractionChain">extraction chain</see>
        /// that match the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the resource to lookup.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ResourceInfo"/> instances representing resources that have successfully been
        /// extracted from the current <see cref="ResourceExtractionChain">chain</see>.
        /// </returns>
        public IEnumerable<ResourceInfo> ExtractAll(string name) => 
            DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty()).Where(x => x != null);
    }
}
