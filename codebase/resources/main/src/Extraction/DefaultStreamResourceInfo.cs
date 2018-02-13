using System;
using System.Globalization;

using Axle.Extensions.Uri;
using Axle.Resources.Streaming;


namespace Axle.Resources.Extraction
{
    public class DefaultStreamResourceInfo : AbstractStreamResourceInfo
    {
        public DefaultStreamResourceInfo(Uri location, string name, CultureInfo culture) : base(location, name, culture) { }

        protected override bool TryGetStreamAdapter(Uri location, out IUriStreamAdapter adapter)
        {
            // TODO: ResX


            #if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
            if (location.IsFile)
            {
                adapter = new FileSystemUriStreamAdapter();
                return true;
            }
            #endif

            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            if (location.IsEmbeddedResource())
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