#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;

namespace Axle.Configuration.ConfigurationManager.Adapters
{
    internal sealed class ConfigurationManagerNameValueSectionAdapter : AbstractConfigurationManagerSectionAdapter
    {
        public ConfigurationManagerNameValueSectionAdapter(NameValueConfigurationCollection nvc, string name) : base(name)
        {
            foreach (var key in nvc.AllKeys)
            {
                var e = nvc[key];
                var value = e.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    Properties.Add(e.Name, ConfigSetting.Create(value));
                }
            }
        }
    }
}
#endif