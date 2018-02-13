using System.Collections.Generic;
using System.Linq;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractResourceExtractor : AbstractAggregateResourceExtractor
    {
        /// <inheritdoc />
        protected sealed override ResourceInfo Aggregate(IEnumerable<ResourceInfo> resources) => resources.SingleOrDefault();
    }
    public abstract class AbstractResourceExtractor<T> : AbstractAggregateResourceExtractor<T> where T: ResourceInfo
    {
        /// <inheritdoc />
        protected sealed override T Aggregate(IEnumerable<T> resources) => resources.SingleOrDefault();
    }
}