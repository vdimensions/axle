#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;

namespace Axle.Configuration.Legacy.Adapters
{
    internal sealed class AppSettings2ConfigSectionAdapter : AbstractConfigSection
    {
        public AppSettings2ConfigSectionAdapter(AppSettingsSection appSettings, string name) : base(name)
        {
            foreach (var k in appSettings.Settings.AllKeys)
            {
                Data.Add(k, new[] { ConfigSetting.Create(appSettings.Settings[k].Value) });
            }
        }
    }
}
#endif