using System.Collections.Generic;


namespace Axle.Resources.Extraction
{
    partial class ResourceContext
    {
        private sealed class ContextExtractionChain : IContextExtractionChain
        {
            private readonly IEnumerable<ResourceContext> _subContexts;
            private readonly IResourceExtractor[] _extractors;
            //private readonly int _extractorsIndex;

            public ContextExtractionChain(IEnumerable<ResourceContext> subContexts, IResourceExtractor[] extractors)
            {
                _subContexts = subContexts;
                _extractors = extractors;
                //_extractorsIndex = extractorsIndex;
            }

            public ResourceInfo Extract(string name)
            {
                foreach (var subContext in _subContexts)
                {
                    ResourceInfo result;
                    foreach (var extractor in _extractors)
                    {
                        result = extractor.Extract(subContext, name);
                        if (result != null)
                        {
                            result.Bundle = subContext.Bundle;
                            return result;
                        }
                    }
                    result = subContext.ExtractionChain.Extract(name);
                    if (result != null)
                    {
                        // The Bundle property was set in the chained extractor, no need to set it again here.
                        //
                        return result;
                    }
                }
                return null;
            }
        }
    }
}
