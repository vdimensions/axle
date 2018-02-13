using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractAggregateResourceExtractor : AbstractAggregateResourceExtractor<ResourceInfo> { }
    public abstract class AbstractAggregateResourceExtractor<T> : IResourceExtractor where T: ResourceInfo
    {
        protected abstract T Aggregate(IEnumerable<T> resources);

        protected abstract T Extract(Uri location, CultureInfo culture, string name);

        private IEnumerable<T> ExtractAll(ResourceExtractionContext ctx, string name)
        {
            foreach (var location in ctx.LookupLocations)
            {
                var result = Extract(location, ctx.Culture, name);
                if (result != null)
                {
                    yield return result;
                }
            }
        }

        /// <inheritdoc />
        public virtual ResourceInfo Extract(ResourceExtractionContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            return Aggregate(ExtractAll(context, name).ToArray());
        }
    }
}