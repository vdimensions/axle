#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Axle.Configuration.Adapters
{
    using MSConfigurationProvider = Microsoft.Extensions.Configuration.ConfigurationProvider;

    /// <summary>
    /// An implementation of the <see cref="IConfigurationProvider"/> which exposes an instance of <see cref="IConfigSection"/>
    /// to be used as configuration source where <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> objects
    /// are required.
    /// </summary>
    internal class AxleConfigurationProvider : MSConfigurationProvider
    {
        private static void Parse(string key, IConfigSetting setting, IDictionary<string, string> data)
        {
            if (setting == null)
            {
                return;
            }
            var isValidKey = !string.IsNullOrEmpty(key);
            if (isValidKey && !string.IsNullOrEmpty(setting.Value))
            {
                data[key] = setting.Value;
            }
            if (setting is IConfigSection section)
            {
                foreach (var sectionKey in section.Keys)
                {
                    var parsedKey = isValidKey ? ConfigurationPath.Combine(key, sectionKey) : sectionKey;
                    Parse(parsedKey, section[sectionKey], data);
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