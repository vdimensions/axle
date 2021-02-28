using System.Collections;
using System.Collections.Generic;

namespace Axle.Resources.Extraction
{
    public abstract class ResourceExtractorRegistryDecorator : IResourceExtractorRegistry
    {
        private readonly IResourceExtractorRegistry _impl;

        protected ResourceExtractorRegistryDecorator(IResourceExtractorRegistry impl)
        {
            _impl = impl;
        }

        public virtual IEnumerator<IResourceExtractor> GetEnumerator() => _impl.GetEnumerator();
            
        public virtual IResourceExtractorRegistry Register(IResourceExtractor extractor) => _impl.Register(extractor);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _impl).GetEnumerator();
    }
}