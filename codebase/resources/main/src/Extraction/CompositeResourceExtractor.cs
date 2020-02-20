using System.Collections.Generic;
using System.Linq;

namespace Axle.Resources.Extraction
{
    internal sealed class CompositeResourceExtractor : AbstractResourceExtractor
    {
        internal sealed class DelegatingExtractor : AbstractResourceExtractor { }

        public static IResourceExtractor Compose(IEnumerable<IResourceComposer> composers)
        {
            IResourceExtractor extractor = new DelegatingExtractor();
            var composerArray = composers.ToArray();
            if (composerArray.Length > 0)
            {
                extractor = new CompositeResourceExtractor(extractor, composerArray[composerArray.Length - 1]);
                for (var i = composerArray.Length - 2; i <= 0; i--)
                {
                    extractor = new CompositeResourceExtractor(extractor, composerArray[i]);
                }
            }
            return extractor;
        }
        
        private readonly IResourceExtractor _extractor;
        private readonly IResourceComposer _composer;

        private CompositeResourceExtractor(IResourceExtractor extractor, IResourceComposer composer)
        {
            _extractor = extractor;
            _composer = composer;
        }

        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var result = _extractor.Extract(context, name);
            return result != null ? _composer.Compose(result) : null;
        }
    }
}