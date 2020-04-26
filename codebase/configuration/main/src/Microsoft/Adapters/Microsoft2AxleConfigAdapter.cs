#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfiguration = global::Microsoft.Extensions.Configuration.IConfiguration;

    /// <summary>
    /// An abstract adapter class that bridges the functionality of an instance of <see cref="IMSConfiguration"/> to
    /// trough the <see cref="IConfigSection"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The actual configuration object from the Microsoft ecosystem.
    /// Must implement the <see cref="IMSConfiguration"/> interface.
    /// </typeparam>
    public abstract class Microsoft2AxleConfigAdapter<T> : IConfigSection where T: IMSConfiguration
    {
        /// <summary>
        /// When called in a derived class, initializes the current <see cref="Microsoft2AxleConfigAdapter{T}"/>
        /// implementation with the provided <paramref name="configuration"/> object. 
        /// </summary>
        /// <param name="configuration"></param>
        protected Microsoft2AxleConfigAdapter(T configuration)
        {
            UnderlyingConfiguration = configuration.VerifyArgument(nameof(configuration)).IsNotNull();
        }

        public IEnumerable<IConfigSection> GetSections(string key)
        {
            key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
            var section = UnderlyingConfiguration.GetSection(key);
            return section == null 
                ? Enumerable.Empty<IConfigSection>() 
                : new IConfigSection[]{ new Microsoft2AxleConfigSectionAdapter(section) };
        }

        /// A reference to the source configuration object that is being adapted.
        internal T UnderlyingConfiguration { get; }
        
        /// <inheritdoc />
        public IEnumerable<string> Keys => UnderlyingConfiguration.GetChildren().Select(x => x.Key);

        /// <inheritdoc />
        public abstract string Value { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public IEnumerable<IConfigSetting> this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                var value = UnderlyingConfiguration[key];
                if (value != null)
                {
                    return new[] { ConfigSetting.Create(value) };
                }
                var section = UnderlyingConfiguration.GetSection(key);
                if (section != null)
                {
                    var result = new Microsoft2AxleConfigSectionAdapter(section);
                    if (!string.IsNullOrEmpty(result.Value) || result.Keys.Any())
                    {
                        return new[] { result };
                    }
                }
                return Enumerable.Empty<IConfigSection>();
            }
        }
    }
}
#endif