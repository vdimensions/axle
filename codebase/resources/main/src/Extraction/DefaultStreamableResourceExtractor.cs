using System;

using Axle.Resources.Streaming;


namespace Axle.Resources.Extraction
{
    public class DefaultStreamableResourceExtractor : AbstractStreamableResourceExtractor
    {
        protected override bool TryGetStreamAdapter(Uri location, out IUriStreamAdapter adapter)
        {
            #if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
            // TODO: ResX
            #endif

            #if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
            if (location.IsFile)
            {
                adapter = new FileSystemUriStreamAdapter();
                return true;
            }
            #endif

            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            if (Axle.Extensions.Uri.UriExtensions.IsEmbeddedResource(location))
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