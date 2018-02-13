using System;
using System.Globalization;

using Axle.Resources.Streaming;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractStreamableResourceExtractor : IResourceExtractor
    {
        protected abstract bool TryGetStreamAdapter(Uri location, out IUriStreamAdapter adapter);

        public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
        {
            if (TryGetStreamAdapter(location, out var adapter))
            {
                resource = new UriStreamAdapterResourceInfo(location, name, culture, adapter);
                return true;
            }
            resource = null;
            return false;
        }
    }
}