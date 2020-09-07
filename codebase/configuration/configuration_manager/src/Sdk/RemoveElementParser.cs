#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Configuration;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    /// <summary>
    /// The default parser for configuration elements within an <see cref="AbstractAddRemoveElementCollection{TAdd,TRemove,T}"/>
    /// implementation that reside in the <c>remove</c> section.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the configuration element within the collection.
    /// It must inherit from <see cref="ConfigurationElement"/> and have a public default constructor.
    /// </typeparam>
    /// <seealso cref="AbstractConfigurationElementParser{T}"/>
    /// <seealso cref="AbstractAddRemoveElementCollection{TAdd,TRemove,T}"/>
    /// <seealso cref="ConfigurationElement"/>
    /// <seealso cref="IConfigurationElementParser"/>
    public class RemoveElementParser<T> : AbstractConfigurationElementParser<T> where T: ConfigurationElement, new()
    {
        public sealed override bool Accept(string elementName)
        {
            return elementName.Equals(AddRemoveOperation.Remove.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
#endif