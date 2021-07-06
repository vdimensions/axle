#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Collections.Generic;
using System.Linq;

namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfiguration = global::Microsoft.Extensions.Configuration.IConfiguration;
    using IMSConfigurationProvider = global::Microsoft.Extensions.Configuration.IConfigurationProvider;
    using MSConfigurationProvider = global::Microsoft.Extensions.Configuration.ConfigurationProvider;
    using MSConfigurationPath = global::Microsoft.Extensions.Configuration.ConfigurationPath;

    /// <summary>
    /// An implementation of the <see cref="IMSConfigurationProvider"/> which exposes an instance of
    /// <see cref="IConfigSection"/> to be used as configuration source where <see cref="IMSConfiguration"/> objects
    /// are required.
    /// </summary>
    internal sealed class AxleConfigurationProvider : MSConfigurationProvider
    {
        private static void Parse(string key, IConfigSetting setting, IDictionary<string, string> data)
        {
            if (setting == null)
            {
                return;
            }
            var isValidKey = !string.IsNullOrEmpty(key);
            if (isValidKey && setting.Value != null && setting.Value.Length > 0)
            {
                // TODO: calls on `CharSequence.ToString()` are a bad idea
                data[key] = setting.Value.ToString();
            }
            if (setting is IConfigSection section)
            {
                foreach (var sectionKey in section.Keys)
                {
                    var parsedKey = isValidKey ? MSConfigurationPath.Combine(key, sectionKey) : sectionKey;
                    foreach (var configSetting in section[sectionKey].Reverse())
                    {
                        Parse(parsedKey, configSetting, data);
                    }
                }
            }
        }

        public AxleConfigurationProvider(IConfigSection rootConfiguration)
        {
            Parse(null, rootConfiguration, Data);
        }
    }
}
#endif