#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Configuration;

namespace Axle.Configuration.Legacy.Sdk
{
    /// <summary>
    /// The default parser for configuration elements within an <see cref="AbstractAddRemoveElementCollection{TAdd,TRemove,T}"/>
    /// implementation that reside in the <c>add</c> section.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the configuration element within the collection.
    /// It must inherit from <see cref="ConfigurationElement"/> and have a public default constructor.
    /// </typeparam>
    /// <seealso cref="AbstractConfigurationElementParser{T}"/>
    /// <seealso cref="AbstractAddRemoveElementCollection{TAdd,TRemove,T}"/>
    /// <seealso cref="IConfigurationElementParser"/>
    /// <seealso cref="ConfigurationElement"/>
    public class AddElementParser<T> : AbstractConfigurationElementParser<T> where T: ConfigurationElement, new()
    {
        public sealed override bool Accept(string elementName)
        {
            return elementName.Equals(AddRemoveOperation.Add.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
#endif