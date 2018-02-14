using System;
using System.Collections.Generic;
using System.Globalization;

using Axle.Extensions.Globalization.CultureInfo;


namespace Axle.Resources.Extraction
{
    public sealed class ResourceContext
    {
        private readonly string _bundle;
        private readonly Uri[] _locations;
        private readonly CultureInfo _culture;

        internal ResourceContext(string bundle, Uri[] locations, CultureInfo culture)
        {
            _bundle = bundle;
            _locations = locations;
            _culture = culture;
        }

        /// <summary>
        /// Splits the current <see cref="ResourceContext"/> instance into multiple <see cref="ResourceContext"/> instances
        /// using a specificed <paramref name="strategy"/>.
        /// </summary>
        /// <param name="strategy">
        /// One of the values of the <see cref="ResourceContextSplitStrategy"/> enumeration.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ResourceContext"/> instances, that determines the order of resolution for composite resources.
        /// </returns>
        public IEnumerable<ResourceContext> Split(ResourceContextSplitStrategy strategy)
        {
            switch (strategy)
            {
                case ResourceContextSplitStrategy.ByLocation:
                    if (_locations.Length <= 1)
                    {
                        yield return this;
                    }
                    else
                    {
                        foreach (var location in _locations)
                        {
                            yield return new ResourceContext(Bundle, new[] { location }, Culture);
                        }
                    }
                    break;
                case ResourceContextSplitStrategy.ByCulture:
                    foreach (var c in _culture.ExpandHierarchy())
                    {
                        yield return new ResourceContext(Bundle, _locations, c);
                    }
                    break;
                case ResourceContextSplitStrategy.ByCultureThenLocation:
                    foreach (var c in _culture.ExpandHierarchy())
                    foreach (var location in _locations)
                    {
                        yield return new ResourceContext(Bundle, new[] { location }, c);
                    }
                    break;
                case ResourceContextSplitStrategy.ByLocationThenCulture:
                    foreach (var location in _locations)
                    foreach (var c in _culture.ExpandHierarchy())
                    {
                        yield return new ResourceContext(Bundle, new[] { location }, c);
                    }
                    break;
            }
        }

        public string Bundle => _bundle;
        public IEnumerable<Uri> LookupLocations => _locations;
        public CultureInfo Culture => _culture;
    }
}
