using System.Collections.Generic;
using System.Linq;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractResourceExtractor : AbstractAggregateResourceExtractor
    {
        protected AbstractResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy)
        {
        }

        /// <inheritdoc />
        protected sealed override ResourceInfo Aggregate(IEnumerable<ResourceInfo> resources) => resources.FirstOrDefault();
    }
    public abstract class AbstractResourceExtractor<T> : AbstractAggregateResourceExtractor<T> where T: ResourceInfo
    {
        protected AbstractResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy)
        {
        }

        /// <inheritdoc />
        protected sealed override T Aggregate(IEnumerable<T> resources) => resources.FirstOrDefault();
    }
}