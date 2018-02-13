using System;
using System.Globalization;


namespace Axle.Resources.Extraction
{
    public class DefaultUriResourceExtractor : IResourceExtractor
    {
        private readonly Uri _location;

        public DefaultUriResourceExtractor(Uri location)
        {
            _location = location;
        }

        public ResourceInfo Extract(string name, CultureInfo culture) => new DefaultStreamResourceInfo(_location, name, culture);
    }
}