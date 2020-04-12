using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Configuration
{
    [Requires(typeof(ConfigSourceRegistry))]
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class ConfigurationModule
    {
        public const string BundleName = "Configuration";

    }
}
