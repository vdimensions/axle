using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Axle.Resources.Extraction
{
    internal sealed class CompositeResourceExtractor : AbstractResourceExtractor
    {
        internal sealed class DelegatingExtractor : AbstractResourceExtractor { }

        private sealed class CompositeResourceContext : IResourceContext
        {
            private readonly IResourceContext _defaultResourceContext;
            private readonly IResourceExtractor _extractor;

            public CompositeResourceContext(IResourceContext defaultResourceContext, IResourceExtractor extractor)
            {
                _defaultResourceContext = defaultResourceContext;
                _extractor = extractor;
            }

            public ResourceInfo Extract(string name) => _extractor.Extract(_defaultResourceContext, name);

            public IEnumerable<ResourceInfo> ExtractAll(string name)
            {
                var currentValue = Extract(name);
                var delegatedResults = _defaultResourceContext.ExtractAll(name);
                return currentValue == null ? delegatedResults : new[] {currentValue}.Union(delegatedResults);
            }

            public string Bundle => _defaultResourceContext.Bundle;
            public Uri Location => _defaultResourceContext.Location;
            public CultureInfo Culture => _defaultResourceContext.Culture;
        }

        public static IResourceExtractor Compose(IEnumerable<IResourceExtractor> extractors)
        {
            var composerArray = extractors.ToArray();
            if (composerArray.Length <= 0)
            {
                return new DelegatingExtractor();
            }
            var extractor = composerArray[0];
            for (var i = 1; i < composerArray.Length; ++i)
            {
                extractor = new CompositeResourceExtractor(extractor, composerArray[i]);
            }
            return extractor;
        }
        
        private readonly IResourceExtractor _extractor1, _extractor2;

        private CompositeResourceExtractor(IResourceExtractor extractor1, IResourceExtractor extractor2)
        {
            _extractor1 = extractor1;
            _extractor2 = extractor2;
        }

        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var ctx = new CompositeResourceContext(context, _extractor1);
            return _extractor2.Extract(ctx, name);
        }
    }
}