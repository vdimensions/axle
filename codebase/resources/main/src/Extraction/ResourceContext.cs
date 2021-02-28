using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Axle.Resources.Bundling;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    public sealed class ResourceContext : IResourceContext
    {
        private static IEnumerable<Tuple<CultureInfo, Uri, IResourceExtractor>> ObtainLookupMap(IResourceBundleContent resourceBundle, CultureInfo culture)
        {
            foreach (var location in resourceBundle.Locations)
            foreach (var extractor in resourceBundle.Extractors.Reverse())
            {
                yield return Tuple.Create(culture, location, extractor);
            }
        }

        public static IResourceContext Create(IResourceBundleContent resourceBundle, CultureInfo culture)
        {
            IResourceContext result = null;
            var bundleName = resourceBundle.Bundle;
            foreach (var rawData in ObtainLookupMap(resourceBundle, culture).Reverse())
            {
                var next = result;
                result = new ResourceContext(bundleName, rawData.Item2, rawData.Item3, rawData.Item1, next);
            }
            return result;
        }

        internal ResourceContext(string bundle, Uri location, IResourceExtractor extractor, CultureInfo culture, IResourceContext next)
        {
            Bundle = bundle;
            Location = location;
            Culture = culture;
            Extractor = extractor;
            Next = next;
        }

        private IEnumerable<ResourceInfo> DoExtractAll(string name)
        {
            var resource = Extractor.Extract(this, name);
            if (resource != null)
            {
                resource.Bundle = Bundle;
                yield return resource;
            }

            if (Next != null)
            {
                foreach (var extracted in Next.ExtractAll(name))
                {
                    yield return extracted;
                }
            }
        }

        ResourceInfo IResourceContext.Extract(string name)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            return DoExtractAll(name).FirstOrDefault(x => x != null);
        }

        IEnumerable<ResourceInfo> IResourceContext.ExtractAll(string name)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            return DoExtractAll(name);
        }

        /// <inheritdoc />
        public string Bundle { get; }
        /// <inheritdoc />
        public Uri Location { get; }
        /// <inheritdoc />
        public CultureInfo Culture { get; }
        /// <inheritdoc />
        public IResourceExtractor Extractor { get; }
        /// <inheritdoc />
        public IResourceContext Next { get; }
    }
}