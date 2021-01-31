using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Extensions.Uri;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A special type of <see cref="IResourceExtractor">resource extractor</see> implementation that delegates
    /// the resource lookup to the extraction chain, but modifies the lookup name for the resources being
    /// looked up. 
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class NameForwardingResourceExtractor : AbstractResourceExtractor
    {
        public NameForwardingResourceExtractor(string pathPrefix)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(pathPrefix, nameof(pathPrefix)));
            PathPrefix = new Uri(pathPrefix, UriKind.Relative);
        }
        public NameForwardingResourceExtractor(Uri pathPrefix)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(pathPrefix, nameof(pathPrefix)));
            PathPrefix = pathPrefix;
        }

        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var path = UriExtensions.Resolve(PathPrefix, name);
            return context.Extract(path.ToString());
        }

        /// <summary>
        /// Gets a <see cref="Uri"/>
        /// </summary>
        public Uri PathPrefix { get; }
    }
}