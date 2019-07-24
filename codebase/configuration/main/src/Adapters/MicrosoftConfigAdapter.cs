#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Adapters
{
    using IMSConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
    using IMSConfigurationSection = Microsoft.Extensions.Configuration.IConfigurationSection;

    public abstract class MicrosoftConfigAdapter<T> : IConfigSection where T: IMSConfiguration
    {
        protected MicrosoftConfigAdapter(T configuration)
        {
            UnderlyingConfiguration = configuration.VerifyArgument(nameof(configuration)).IsNotNull();
        }

        public IConfigSection GetSection(string key)
        {
            key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
            var section = UnderlyingConfiguration.GetSection(key);
            return section == null ? null : new MicrosoftConfigSectionAdapter(section);
        }

        public IEnumerable<string> Keys => UnderlyingConfiguration.GetChildren().Select(x => x.Key);

        public IConfigSetting this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                var value = UnderlyingConfiguration[key];
                if (value != null)
                {
                    return ConfigSetting.Create(value);
                }
                var section = UnderlyingConfiguration.GetSection(key);
                return section == null ? null : new MicrosoftConfigSectionAdapter(section);
            }
        }

        internal T UnderlyingConfiguration { get; }
        public abstract string Value { get; }
        public abstract string Name { get; }
    }

    public sealed class MicrosoftConfigSectionAdapter : MicrosoftConfigAdapter<IMSConfigurationSection>
    {
        public MicrosoftConfigSectionAdapter(IMSConfigurationSection configuration) : base(configuration)
        {
        }

        public override string Value => UnderlyingConfiguration.Value;
        public override string Name => UnderlyingConfiguration.Key;
    }
}
#endif