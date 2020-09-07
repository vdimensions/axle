#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;
using System.Xml;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    /// <summary>
    /// An interface for custom configuration element parsers. 
    /// A configuration element parser is used to create the appropriate configuration element instance in case where a
    /// <see cref="ConfigurationElementCollection" /> implementation can hold elements of different types. 
    /// </summary>
    /// <seealso cref="ConfigurationElement"/>
    /// <seealso cref="ConfigurationElementCollection"/>
    /// <seealso cref="AbstractConfigurationElementCollection"/>
    public interface IConfigurationElementParser
    {
        /// <summary>
        /// Determines if an element is supported by the current <see cref="IConfigurationElementParser"/> implementation.
        /// </summary>
        /// <param name="elementName">The element's tag name.</param>
        /// <returns>
        /// true if the element can be parsed by this <see cref="IConfigurationElementParser"/> instance; otherwise false.
        /// </returns>
        bool Accept(string elementName);

        /// <summary>
        /// Parses the contents of the configuration <see cref="XmlReader" /> to produce a valid configuration element.
        /// </summary>
        /// <param name="reader">The <see cref="System.Xml.XmlReader"/> that reads from the configuration file</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>A successfully parsed configuration element instance.</returns>
        /// <see also="System.Xml.XmlReader"/>
        ConfigurationElement Parse(XmlReader reader, bool serializeCollectionKey);
    }

    /// <summary>
    /// A generic interface for custom configuration element parsers. 
    /// A configuration element parser is used to create the appropriate configuration element instance in case where a
    /// <see cref="ConfigurationElementCollection" /> implementation can hold elements of different types. 
    /// </summary>
    /// <typeparam name="T">
    /// The type of the parsed configuration element. This must be a class that inherits from <see cref="ConfigurationElement" />
    /// </typeparam>
    /// <seealso cref="IConfigurationElementParser"/>
    /// <seealso cref="ConfigurationElement"/>
    /// <seealso cref="ConfigurationElementCollection"/>
    /// <seealso cref="AbstractConfigurationElementCollection"/>
    public interface IConfigurationElementParser<T> : IConfigurationElementParser where T: ConfigurationElement
    {
        /// <summary>
        /// Parses the contents of the configuration <see cref="XmlReader" /> to produce a valid configuration element.
        /// </summary>
        /// <param name="reader">The <see cref="System.Xml.XmlReader"/> that reads from the configuration file</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>A successfully parsed configuration element instance.</returns>
        /// <see also="System.Xml.XmlReader"/>
        new T Parse(XmlReader reader, bool serializeCollectionKey);
    }
}
#endif