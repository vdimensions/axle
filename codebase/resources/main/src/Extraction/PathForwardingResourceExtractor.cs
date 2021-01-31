using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A special type of <see cref="IResourceExtractor">resource extractor</see> implementation that delegates
    /// the resource lookup to the extraction chain, but modifies the lookup location for the resources being
    /// looked up. 
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class PathForwardingResourceExtractor : AbstractResourceExtractor
    {
        private sealed class PathForwardingResourceContext : IResourceContext
        {
            private readonly IResourceContext _impl;

            public PathForwardingResourceContext(IResourceContext impl, Uri location)
            {
                _impl = impl;
                Location = location ?? _impl.Location;
            }

            ResourceInfo IResourceContext.Extract(string name) => _impl.Extract(name);

            IEnumerable<ResourceInfo> IResourceContext.ExtractAll(string name) => _impl.ExtractAll(name);

            string IResourceContext.Bundle => _impl.Bundle;
            public Uri Location { get; }
            CultureInfo IResourceContext.Culture => _impl.Culture;
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
            var pathForwardingContext = new PathForwardingResourceContext(
                context, 
                GetForwardedLocation(context.Location));
            return _extractor.Extract(pathForwardingContext, name);
        }
    }
}