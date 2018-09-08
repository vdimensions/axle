using System.Collections.Generic;
using System.Linq;

using Axle.References;
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
        private readonly ResourceContext _ownerContext;
        private readonly IEnumerable<ResourceContext> _subContexts;
        internal readonly IEnumerable<IResourceExtractor> extractors;

        internal ContextExtractionChain(ResourceContext ownerContext, IEnumerable<ResourceContext> subContexts, IEnumerable<IResourceExtractor> extractors)
        {
            _ownerContext = ownerContext;
            _subContexts = subContexts;
            this.extractors = extractors;
        }

        private IEnumerable<Nullsafe<ResourceInfo>> DoExtractAll(string name)
        {
            var extractorContext = _ownerContext;
            foreach (var extractor in extractors)
            {
                extractorContext = extractorContext.MoveOneExtractorForward();
                var resource = extractor.Extract(extractorContext, name);
                if (!resource.HasValue)
                {
                    continue;
                }
                var r = resource.Value;
                r.Bundle = _ownerContext.Bundle;
                yield return Nullsafe<ResourceInfo>.Some(r);
            }
            foreach (var subContext in _subContexts)
            {
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
        public Nullsafe<ResourceInfo> Extract(string name) => 
            DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty()).FirstOrDefault(x => x.HasValue);

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
        public IEnumerable<Nullsafe<ResourceInfo>> ExtractAll(string name) => 
            DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty()).Where(x => x.HasValue);
    }
}
