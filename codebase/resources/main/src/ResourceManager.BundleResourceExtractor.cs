#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System;
using System.Globalization;

using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources
{
    partial class ResourceManager
    {
        private sealed class BundleResourceExtractor : IResourceExtractor
        {
            private readonly string _bundle;
            private readonly IResourceExtractor _originalExtractor;

            public BundleResourceExtractor(string bundle, IResourceExtractor originalExtractor)
            {
                _bundle = bundle;
                _originalExtractor = originalExtractor;
            }

            public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
            {
                name.VerifyArgument(nameof(name)).IsNotNull();

                if (_originalExtractor.TryExtract(location, name, culture, out resource))
                {
                    resource.Bundle = _bundle;
                    return true;
                }

                resource = null;
                return false;
            }
        }
    }
}
#endif