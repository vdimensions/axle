#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class GenericConfigurationElementParser<T> : AbstractConfigurationElementParser<T> where T: ConfigurationElement, new()
    {
        private readonly string _name;

        public GenericConfigurationElementParser(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override bool Accept(string elementName) => elementName.Equals(_name);
    }
}
#endif