using System;

using Axle.Resources.Extraction.Streaming;


namespace Axle.Resources.Extraction
{
    public class DefaultStreamableResourceExtractor : AbstractStreamableResourceExtractor
    {
        public DefaultStreamableResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy)
        {
        }

        protected override bool TryGetStreamAdapter(Uri location, out IUriStreamAdapter adapter)
        {
            #if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
            if (location.IsAbsoluteUri && location.IsFile)
            {
                adapter = new FileSystemUriStreamAdapter();
                return true;
            }
            #endif

            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            if (location.IsAbsoluteUri && Axle.Extensions.Uri.UriExtensions.IsEmbeddedResource(location))
            {
                adapter = new EmbeddedResourceUriStreamAdapter();
                return true;
            }
            #endif

            adapter = null;
            return false;
        }
    }
}