#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfiguration = global::Microsoft.Extensions.Configuration.IConfiguration;

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

        internal T UnderlyingConfiguration { get; }
        
        /// <inheritdoc />
        public IEnumerable<string> Keys => UnderlyingConfiguration.GetChildren().Select(x => x.Key);

        /// <inheritdoc />
        public abstract string Value { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
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
                if (section != null)
                {
                    var result = new MicrosoftConfigSectionAdapter(section);
                    if (!string.IsNullOrEmpty(result.Value) || result.Keys.Any())
                    {
                        return result;
                    }
                }
                return null;
            }
        }
    }
}
#endif