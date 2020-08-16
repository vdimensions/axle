using System.Collections.Generic;

namespace Axle.Resources.Configuration
{
    internal sealed class ResourcesConfig
    {
        public BundleConfig DefaultBundle { get; } = new BundleConfig { Name = string.Empty };
        public IList<BundleConfig> Bundles { get; set; } = new List<BundleConfig>();
    }
}