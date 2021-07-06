using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Axle.Resources.Bundling;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    internal sealed class ResourceContext : IResourceContext
    {
        private static IEnumerable<Tuple<Uri, IResourceExtractor>> ObtainLookupMap(IResourceBundleContent resourceBundle)
        {
            foreach (var location in resourceBundle.Locations)
            foreach (var extractor in resourceBundle.Extractors.Reverse())
            {
                yield return Tuple.Create(location, extractor);
            }
        }

        public static IResourceContext Create(IResourceBundleContent resourceBundle, CultureInfo culture)
        {
            var bundleName = resourceBundle.Bundle;
            var lookupMap = ObtainLookupMap(resourceBundle).Reverse().ToArray();
            var location = lookupMap[0].Item1;
            var extractor = lookupMap[0].Item2;
            IResourceContext result = new ResourceContext(bundleName, location, new NoopResourceExtractor(), culture, null);
            for (var i = 1; i < lookupMap.Length; i++)
            {
                location = lookupMap[i].Item1;
                var next = result;
                result = new ResourceContext(bundleName, location, extractor, culture, next);
                extractor = lookupMap[i].Item2;
            }
            return new ResourceContext(bundleName, null, extractor, culture, result);
        }

        private ResourceContext(string bundle, Uri location, IResourceExtractor extractor, CultureInfo culture, IResourceContext next)
        {
            Bundle = bundle;
            Location = location;
            Culture = culture;
            Extractor = extractor;
            Next = next;
        }

        private IEnumerable<ResourceInfo> DoExtractAll(string name)
        {
            if (Next != null)
            {
                var resource = Extractor.Extract(Next, name);
                if (resource != null)
                {
                    resource.Bundle = Bundle;
                    yield return resource;
                }

                if (Next.Next != null)
                {
                    foreach (var extracted in Next.ExtractAll(name))
                    {
                        yield return extracted;
                    }
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