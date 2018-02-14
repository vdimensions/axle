using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    [Obsolete("Not used")]
    internal sealed class CompositeResourceExtractor : IResourceExtractor
    {
        private readonly IEnumerable<IResourceExtractor> _extractors;

        public CompositeResourceExtractor(IEnumerable<IResourceExtractor> extractors)
        {
            _extractors = extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value;
        }

        public ResourceInfo Extract(ResourceContext context, string name) => _extractors.Select(x => x.Extract(context, name)).SingleOrDefault();
    }
}