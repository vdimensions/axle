using System;
using System.Globalization;

using Axle.Resources.Extraction.Streaming;


namespace Axle.Resources.Extraction
{
    public class EmbeddedResourceExtractor : AbstractStreamableResourceExtractor
    {
        public EmbeddedResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy) { }

        protected override bool TryGetStreamAdapter(Uri location, CultureInfo culture, string name, out IUriStreamAdapter adapter)
        {
            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            if (location.IsAbsoluteUri && Axle.Extensions.Uri.UriExtensions.IsEmbeddedResource(location) && 
                EmbeddedResourceUriStreamAdapter.TryCreate(location, culture, name, out var erusa))
            {
                adapter = erusa;
                return true;
            }
            #endif

            adapter = null;
            return false;
        }
    }
}