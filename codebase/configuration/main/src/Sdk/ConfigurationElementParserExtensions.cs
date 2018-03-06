using System.Configuration;
using System.Xml;


namespace Axle.Configuration.Sdk
{
    internal static class ConfigurationElementParserExtensions
    {
        public static ConfigurationElement Parse(this IConfigurationElementParser @this, XmlReader reader)
        {
            return @this.Parse(reader, false);
        }
        public static T Parse<T>(this IConfigurationElementParser<T> @this, XmlReader reader) where T: ConfigurationElement
        {
            return @this.Parse(reader, false);
        }
    }
}