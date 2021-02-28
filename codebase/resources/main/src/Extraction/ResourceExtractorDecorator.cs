using System.Threading.Tasks;

namespace Axle.Resources.Extraction
{
    public abstract class ResourceExtractorDecorator : IResourceExtractor
    {
        private readonly IResourceExtractor _impl;

        protected ResourceExtractorDecorator(IResourceExtractor impl)
        {
            _impl = impl;
        }

        public virtual ResourceInfo Extract(IResourceContext context, string name) => _impl.Extract(context, name);

        public virtual Task<ResourceInfo> ExtractAsync(IResourceContext context, string name) => _impl.ExtractAsync(context, name);

        public IResourceExtractor Target => _impl;
    }
}