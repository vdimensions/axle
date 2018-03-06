using System;
using System.Configuration;
using System.Diagnostics;
using System.Xml;

using Axle.Verification;


namespace Axle.Configuration.Sdk
{
    /// <summary>
    /// An abstract class designed serve as a base for creating configuration element objects.
    /// Inherits from <see cref="ConfigurationElement" />
    /// </summary>
    /// <seealso cref="ConfigurationElement"/>
    public abstract class AbstractConfigurationElement : ConfigurationElement, ISupportsDeserializeInternal
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

        protected AbstractConfigurationElement()
        {
            RegisterProperties(Properties);
        }

        protected virtual void RegisterProperties(ConfigurationPropertyCollection properties) { }

        protected T Resolve<T>(ConfigurationProperty property)
        {
            return (T) base[property.VerifyArgument(nameof(property)).IsNotNull().Value];
        }

        void ISupportsDeserializeInternal.Deserialize(XmlReader reader, bool serializeCollectionKey) => DeserializeElement(reader, serializeCollectionKey);

        protected sealed override ConfigurationPropertyCollection Properties => _configurationProperties;
    }
}