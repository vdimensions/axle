using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Axle.Resources.Extraction
{
    public sealed partial class ResourceContext
    {
        private readonly Uri[] _locations;
        private readonly int _currentLocationIndex;

        internal ResourceContext(string bundle, Uri[] locations, CultureInfo culture, IResourceExtractor[] extractors)
            : this(bundle, locations, -1, culture, extractors) { }
        private ResourceContext(
                string bundle, 
                Uri[] locations, 
                int currentLocationIndex, 
                CultureInfo culture, 
                IResourceExtractor[] extractors)
        {
            Bundle = bundle;
            _locations = locations;
            _currentLocationIndex = currentLocationIndex;
            Culture = culture;

            var subContexts = GetSubContexts(extractors).ToArray();
            ExtractionChain = new ContextExtractionChain(subContexts, extractors);
        }

        private IEnumerable<ResourceContext> GetSubContexts(IResourceExtractor[] extractors)
        {
            for (var i = _currentLocationIndex + 1; i < _locations.Length; i++)
            {
                yield return new ResourceContext(Bundle, _locations, i, Culture, extractors);
            }
        }

        public string Bundle { get; }
        public Uri Location => _currentLocationIndex < 0 ? null :_locations[_currentLocationIndex];
        //public IEnumerable<Uri> LookupLocations => _locations;
        public CultureInfo Culture { get; }
        public IContextExtractionChain ExtractionChain { get; }
    }
}
