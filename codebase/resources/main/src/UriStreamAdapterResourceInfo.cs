using System;
using System.Globalization;
using System.IO;

using Axle.Resources.Streaming;


namespace Axle.Resources
{
    internal class UriStreamAdapterResourceInfo : ResourceInfo
    {
        private readonly IUriStreamAdapter _adapter;

        public UriStreamAdapterResourceInfo(Uri location, string name, CultureInfo culture, IUriStreamAdapter adapter) 
            : base(location, name, culture, "application/octet-stream")
        {
            _adapter = adapter;
        }

        /// <inheritdoc />
        public override Stream Open() => Location == null ? _adapter.GetStream(Location) : null;
    }
}