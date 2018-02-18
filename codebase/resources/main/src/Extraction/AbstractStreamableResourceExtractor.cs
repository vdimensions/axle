using System;
using System.Globalization;
using System.IO;

using Axle.Extensions.Uri;
using Axle.Resources.Extraction.Streaming;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractStreamableResourceExtractor : AbstractResourceExtractor
    {
        private class UriStreamAdapterResourceInfo : ResourceInfo
        {
            private readonly Uri _location;
            private readonly IUriStreamAdapter _adapter;

            public UriStreamAdapterResourceInfo(Uri location, string name, CultureInfo culture, IUriStreamAdapter adapter)
                : base(name, culture, "application/octet-stream")
            {
                _location = location.Resolve(name);
                _adapter = adapter;
            }

            /// <inheritdoc />
            public override Stream Open()
            {
                return _location != null ? _adapter.GetStream(_location) : null;
            }
        }

        protected AbstractStreamableResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy)
        {
        }

        protected abstract bool TryGetStreamAdapter(Uri location, CultureInfo culture, string name, out IUriStreamAdapter adapter);

        protected sealed override ResourceInfo Extract(Uri location, CultureInfo culture, string name)
        {
            return TryGetStreamAdapter(location, culture, name, out var adapter)
                ? new UriStreamAdapterResourceInfo(location, name, culture, adapter)
                : null;
        }
    }
}