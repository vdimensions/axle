#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;

namespace Axle.Configuration.ConfigurationManager.Adapters
{
    internal sealed class ConnectionStringSettings2ConfigSectionAdapter : AbstractConfigSection
    {
        public ConnectionStringSettings2ConfigSectionAdapter(ConnectionStringSettings settings, string name) 
            : base(name)
        {
            if (!string.IsNullOrEmpty(settings.Name))
            {
                Data.Add(nameof(ConnectionStringSettings.Name), new[] { ConfigSetting.Create(settings.Name) });
            }
            if (!string.IsNullOrEmpty(settings.ConnectionString))
            {
                Data.Add(
                    nameof(ConnectionStringSettings.ConnectionString), 
                    new[] { ConfigSetting.Create(settings.ConnectionString) });
            }
            if (!string.IsNullOrEmpty(settings.ProviderName))
            {
                Data.Add(nameof(ConnectionStringSettings.ProviderName), new[]{ ConfigSetting.Create(settings.ProviderName) });
            }
        }
    }
}
#endif