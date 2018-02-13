using System;
using System.Collections.Generic;
using System.Globalization;


namespace Axle.Resources.Extraction
{
    public delegate ResourceInfo ResourceLookupFunc(Uri location, CultureInfo culture, string name);

    public sealed class ResourceExtractionContext
    {
        private readonly string _bundle;
        private readonly Uri[] _locations;
        private readonly CultureInfo _culture;

        internal ResourceExtractionContext(string bundle, Uri[] locations, CultureInfo culture)
        {
            _bundle = bundle;
            _locations = locations;
            _culture = culture;
        }

        public IEnumerable<ResourceExtractionContext> Split()
        {
            if (_locations.Length <= 1)
            {
                yield return this;
            }
            else
            {
                foreach (var location in LookupLocations)
                {
                    yield return new ResourceExtractionContext(Bundle, new[] { location }, Culture);
                }
            }
        }

        public string Bundle => _bundle;
        public IEnumerable<Uri> LookupLocations => _locations;
        public CultureInfo Culture => _culture;
    }
}
