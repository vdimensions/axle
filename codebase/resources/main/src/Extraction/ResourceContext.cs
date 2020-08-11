using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Axle.Verification;

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
    internal sealed class ResourceContext : IResourceContext
    {
        private readonly Uri[] _locations;
        private readonly int _currentLocationIndex;

        internal ResourceContext(
                string bundle, 
                Uri[] locations, 
                CultureInfo culture, 
                IEnumerable<IResourceExtractor> extractors)
            : this(bundle, locations, 0, culture, extractors) { }
            //: this(bundle, locations, -1, culture, extractors) { }
        private ResourceContext(
                string bundle, 
                Uri[] locations, 
                int currentLocationIndex, 
                CultureInfo culture, 
                IEnumerable<IResourceExtractor> extractors)
        {
            Bundle = bundle;
            _locations = locations;
            _currentLocationIndex = currentLocationIndex;
            Culture = culture;

            var extArr = extractors.ToArray();
            var subContexts = GetSubContexts(extArr).ToArray();
            ExtractionChain = new ResourceExtractionChain(this, subContexts, extArr);
        }

        private IEnumerable<ResourceContext> GetSubContexts(IResourceExtractor[] extractors)
        {
            for (var i = _currentLocationIndex + 1; i < _locations.Length; i++)
            {
                yield return new ResourceContext(Bundle, _locations, i, Culture, extractors);
            }
        }

        internal ResourceContext MoveOneExtractorForward()
        {
            var extractors = ExtractionChain.Extractors.Skip(1);
            return new ResourceContext(Bundle, _locations, _currentLocationIndex, Culture, extractors);
        }

        /// <inheritdoc />
        public ResourceInfo Extract(string name)
        {
            return ExtractionChain
                .DoExtractAll(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty())
                .FirstOrDefault(x => x != null);
        }

        /// <inheritdoc />
        public IEnumerable<ResourceInfo> ExtractAll(string name) => 
            ExtractionChain.DoExtractAll(name.VerifyArgument(nameof(name))).Where(x => x != null);

        /// <inheritdoc />
        public string Bundle { get; }

        /// <inheritdoc />
        public Uri Location => _currentLocationIndex < 0 ? null : _locations[_currentLocationIndex];

        /// <inheritdoc />
        public CultureInfo Culture { get; }

        /// <summary>
        /// Gets the <see cref="ResourceExtractionChain"/> associated with the current <see cref="ResourceContext"/>
        /// instance.
        /// The extraction chain can be accessed during resource extraction to obtain any additional resources that may
        /// be required to construct the final resource.
        /// </summary>
        internal ResourceExtractionChain ExtractionChain { get; }
    }
}