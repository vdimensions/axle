using System;

namespace Axle.Resources.Configuration
{
    internal sealed class BundleConfig
    {
        public string Name { get; set; }
        public Uri[] Locations { get; set; }
    }
}