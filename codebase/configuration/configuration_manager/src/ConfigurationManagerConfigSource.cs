#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.ConfigurationManager.Adapters;

namespace Axle.Configuration.ConfigurationManager
{
    /// <summary>
    /// An <see cref="IConfigSource"/> implementation that uses configuration obtained from the
    /// <see cref="System.Configuration.ConfigurationManager"/>. 
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class ConfigurationManagerConfigSource : IConfigSource
    {
        /// <inheritdoc />
        public IConfiguration LoadConfiguration()
        {
            var cfg = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return new ConfigurationManagerSection2ConfigurationAdapter(cfg);
        }
    }
}
#endif