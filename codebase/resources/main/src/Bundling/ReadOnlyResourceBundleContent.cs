using System;
using System.Collections.Generic;
using Axle.Resources.Extraction;

namespace Axle.Resources.Bundling
{
    public sealed class ReadOnlyResourceBundleContent : IResourceBundleContent
    {
        private readonly IResourceBundleContent _impl;

        public ReadOnlyResourceBundleContent(IResourceBundleContent impl)
        {
            _impl = impl;
        }

        public IEnumerable<Uri> Locations => _impl.Locations;

        public IEnumerable<IResourceExtractor> Extractors => _impl.Extractors;

        public string Bundle => _impl.Bundle;
    }
}