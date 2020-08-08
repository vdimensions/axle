namespace Axle.Resources.Configuration
{
    internal sealed class ResourcesConfig
    {
        public BundleConfig DefaultBundle { get; } = new BundleConfig { Name = string.Empty };
        public BundleConfig[] Bundles { get; set; }
    }
}