using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractAggregateResourceExtractor : AbstractAggregateResourceExtractor<ResourceInfo>
    {
        protected AbstractAggregateResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy)
        {
        }
    }
    public abstract class AbstractAggregateResourceExtractor<T> : IResourceExtractor where T: ResourceInfo
    {
        private readonly ResourceContextSplitStrategy _splitrStrategy;

        protected AbstractAggregateResourceExtractor(ResourceContextSplitStrategy splitrStrategy)
        {
            _splitrStrategy = splitrStrategy;
        }

        protected abstract T Aggregate(IEnumerable<T> resources);

        protected abstract T Extract(Uri location, CultureInfo culture, string name);

        private IEnumerable<T> ExtractAll(ResourceContext ctx, string name)
        {
            foreach (var c in ctx.Split(_splitrStrategy))
            {
                var result = Extract(c.LookupLocations.First(), c.Culture, name);
                if (result != null)
                {
                    yield return result;
                }
            }
        }

        /// <inheritdoc />
        public virtual ResourceInfo Extract(ResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            return Aggregate(ExtractAll(context, name));
        }
    }
}