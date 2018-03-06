using System;
using System.Configuration;


namespace Axle.Configuration.Sdk
{
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