using System;
using System.Collections.Generic;

namespace Axle.Resources.Configuration
{
    internal sealed class ResourcesConfig
    {
        public DefaultBundleConfig DefaultBundle { get; } = new DefaultBundleConfig { };
        public IList<CustomBundleConfig> Bundles { get; set; } = new List<CustomBundleConfig>();
        public Type CacheManager { get; set; }
    }
}