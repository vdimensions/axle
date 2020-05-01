using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Extensions.Uri;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A special type of <see cref="IResourceExtractor">resource extractor</see> implementation that causes resource
    /// delegates resource lookup to the extraction chain, but modifies the lookup location for the resources being
    /// looked up. 
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class PathForwardingResourceExtractor : AbstractResourceExtractor
    {
        public PathForwardingResourceExtractor(string prefixPath)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(prefixPath, nameof(prefixPath)));
            PathPrefix = new Uri(prefixPath, UriKind.Relative);
        }
        public PathForwardingResourceExtractor(Uri pathPrefix)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(pathPrefix, nameof(pathPrefix)));
            PathPrefix = pathPrefix;
        }

        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var path = PathPrefix.Resolve(name);
            return context.Extract(path.ToString());
        }

        public Uri PathPrefix { get; }
    }
}