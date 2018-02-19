using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A class that provides the context for resource lookup and extraction, including the <see cref="Location">location</see>
    /// where the resource should be looked up, the <see cref="Culture">culture</see> for which the resource has been requested, 
    /// and an <see cref="ExtractionChain">extraction chain</see> that allows for querying up resources that would act as 
    /// components for producing the requested resource.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    /// <seealso cref="ResourceManager"/>
    public sealed class ResourceContext
    {
        private readonly Uri[] _locations;
        private readonly int _currentLocationIndex;

        internal ResourceContext(string bundle, Uri[] locations, CultureInfo culture, IResourceExtractor[] extractors)
            : this(bundle, locations, -1, culture, extractors) { }
        private ResourceContext(string bundle, Uri[] locations, int currentLocationIndex, CultureInfo culture, IResourceExtractor[] extractors)
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

        /// <summary>
        /// Gets the name of the resource bundle this <see cref="ResourceContext">resource context</see> instance is representing.
        /// </summary>
        public string Bundle { get; }

        /// <summary>
        /// Gets the <see cref="Uri"/> for the resource lookup location of the current <see cref="ResourceContext"/> instance.
        /// </summary>
        public Uri Location => _currentLocationIndex < 0 ? null :_locations[_currentLocationIndex];

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> representing the culture that the current <see cref="ResourceContext"/> instance will use
        /// for resource lookup.
        /// </summary>
        public CultureInfo Culture { get; }

        /// <summary>
        /// Gets the <see cref="ContextExtractionChain"/> associated with the current <see cref="ResourceContext"/> instance.
        /// The extraction chain can be accessed during resource extraction to obtain any additional resources that may be required to 
        /// costruct the final resource.
        /// </summary>
        public ContextExtractionChain ExtractionChain { get; }
    }
}
