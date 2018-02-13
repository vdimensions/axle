using System;
using System.Globalization;
using System.IO;

using Axle.Resources.Streaming;


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
                _location = location;
                _adapter = adapter;
            }

            /// <inheritdoc />
            public override Stream Open() => _location == null ? _adapter.GetStream(_location) : null;
        }

        protected abstract bool TryGetStreamAdapter(Uri location, out IUriStreamAdapter adapter);

        protected sealed override ResourceInfo Extract(Uri location, CultureInfo culture, string s)
        {
            return TryGetStreamAdapter(location, out var adapter)
                ? new UriStreamAdapterResourceInfo(location, s, culture, adapter)
                : null;
        }
    }
}