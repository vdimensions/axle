using System;
using System.Configuration;


namespace Axle.Configuration.Sdk
{
    public class GenericConfigurationElementParser<T> : AbstractConfigurationElementParser<T> where T: ConfigurationElement, new()
    {
        private readonly string name;

        public GenericConfigurationElementParser(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            this.name = name;
        }

        public override bool Accept(string elementName) { return elementName.Equals(name); }
    }
}