using System;
using System.Globalization;
using System.IO;

using Axle.Resources.Streaming;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractStreamResourceInfo : ResourceInfo
    {
        protected AbstractStreamResourceInfo(Uri location, string name, CultureInfo culture) : base(location, name, culture, "application/octet-stream") { }

        protected abstract bool TryGetStreamAdapter(Uri location, out IUriStreamAdapter adapter);

        /// <inheritdoc />
        public override Stream Open() => Location == null && TryGetStreamAdapter(Location, out var adapter) ? adapter.GetStream(Location) : null;
    }
}