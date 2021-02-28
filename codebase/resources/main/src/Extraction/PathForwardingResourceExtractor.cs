using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A special type of <see cref="IResourceExtractor">resource extractor</see> implementation that delegates
    /// the resource lookup to the extraction chain, but modifies the lookup location for the resources being
    /// looked up. 
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class PathForwardingResourceExtractor : AbstractResourceExtractor
    {
        private sealed class PathForwardingResourceContext : ResourceContextDecorator
        {
            public static IResourceContext Create(IResourceContext impl, Func<Uri, Uri> locationTransform)
            {
                var location = locationTransform(impl.Location);
                if (location == null || location.Equals(impl.Location))
                {
                    return impl;
                }

                if (impl.Next == null)
                {
                    return new PathForwardingResourceContext(impl, location, null);
                }
                else
                {
                    return new PathForwardingResourceContext(impl, location, Create(impl.Next, locationTransform));
                }
            }
            
            private readonly Uri _location;
            private readonly IResourceContext _next;

            public PathForwardingResourceContext(IResourceContext impl, Uri location, IResourceContext next) : base(impl)
            {
                _location = location;
                _next = next;
            }

            public override Uri Location => _location;
            public override IResourceContext Next => _next;
        }

        private readonly IResourceExtractor _extractor;
        
        public PathForwardingResourceExtractor(IResourceExtractor extractor)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(extractor, nameof(extractor)));
            _extractor = extractor;
        }

        protected virtual Uri GetForwardedLocation(Uri location) => location;

        protected sealed override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var pathForwardingContext = PathForwardingResourceContext.Create(context, GetForwardedLocation);
            return _extractor.Extract(pathForwardingContext, name);
        }
    }
}