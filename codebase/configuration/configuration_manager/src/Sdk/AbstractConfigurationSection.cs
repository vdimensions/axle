#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Configuration;
using System.Diagnostics;
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    [Serializable]
    public abstract class AbstractConfigurationSection : ConfigurationSection
    {
        protected static ConfigurationPropertyBuilder<T> CreateProperty<T>(string name)
        {
            return ConfigurationPropertyBuilder<T>.Create(name);
        }
        protected static ConfigurationPropertyBuilder CreateProperty(string name, Type type)
        {
            return ConfigurationPropertyBuilder.Create(name).OfType(type);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConfigurationPropertyCollection _configurationProperties = new ConfigurationPropertyCollection();

        protected AbstractConfigurationSection()
        {
            RegisterProperties(this.Properties);
        }

        protected virtual void RegisterProperties(ConfigurationPropertyCollection properties) { }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        protected T Resolve<T>(ConfigurationProperty property)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(property, nameof(property)));
            return (T) base[property];
        }

        protected sealed override ConfigurationPropertyCollection Properties => _configurationProperties;
    }
}
#endif