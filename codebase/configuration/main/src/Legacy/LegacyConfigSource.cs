#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.Legacy.Adapters;

namespace Axle.Configuration.Legacy
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class LegacyConfigSource : IConfigSource
    {
        public IConfiguration LoadConfiguration()
        {
            var cfg = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return new LegacyConfiguration2ConfigurationAdapter(cfg);
        }
    }
}
#endif