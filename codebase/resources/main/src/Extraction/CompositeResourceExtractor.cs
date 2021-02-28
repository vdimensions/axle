using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Axle.Resources.Extraction
{
    public sealed class CompositeResourceExtractor : AbstractResourceExtractor
    {
        private sealed class CompositeResourceContext : IResourceContext
        {
            private static IResourceContext CreateContextChain(CompositeResourceExtractor master, IResourceContext context)
            {
                if (context.Extractor is ResourceExtractorDecorator decorator && ReferenceEquals(master, decorator.Target))
                {
                    return context.Next;
                }
                return context;
            }
            
            public static IResourceContext Create(CompositeResourceExtractor master, IResourceContext context, IResourceExtractor otherExtractor)
            {
                return new CompositeResourceContext(CreateContextChain(master, context), otherExtractor);
            }
            
            private readonly IResourceContext _sourceContext;
            private readonly IResourceExtractor _composedExtractor;

            private CompositeResourceContext(IResourceContext sourceContext, IResourceExtractor composedExtractor)
            {
                _sourceContext = sourceContext;
                _composedExtractor = composedExtractor;
            }

            public ResourceInfo Extract(string name) => _composedExtractor.Extract(_sourceContext, name);

            public IEnumerable<ResourceInfo> ExtractAll(string name)
            {
                var currentValue = Extract(name);
                var delegatedResults = _sourceContext.ExtractAll(name);
                return currentValue == null ? delegatedResults : new[] {currentValue}.Union(delegatedResults);
            }

            public string Bundle => _sourceContext.Bundle;
            public Uri Location => _sourceContext.Location;
            public CultureInfo Culture => _sourceContext.Culture;
            public IResourceExtractor Extractor => _composedExtractor;
            public IResourceContext Next => _sourceContext.Next;
        }

        public static IResourceExtractor Create(params IResourceExtractor[] extractors) 
            => Create(extractors as IEnumerable<IResourceExtractor>);
        public static IResourceExtractor Create(IEnumerable<IResourceExtractor> extractors)
        {
            var composerArray = extractors.ToArray();
            if (composerArray.Length == 0)
            {
                return new NoopResourceExtractor();
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
            var ctx = CompositeResourceContext.Create(this, context, _extractor1);
            return _extractor2.Extract(ctx, name);
        }
    }
}