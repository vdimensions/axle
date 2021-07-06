#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Configuration;
using System.Diagnostics;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    public class ConfigurationElementCollection<T> : AbstractConfigurationElementCollection<T>, IConfigurationElementCollection<T> where T: ConfigurationElement
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConfigurationElementCollectionType _collectionType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _elementName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<T, object> _getElementKeyFunc;

        public ConfigurationElementCollection(string elementName, Func<T, object> getElementKeyFunc) 
            : this(ConfigurationElementCollectionType.BasicMap, elementName, getElementKeyFunc) { }
        public ConfigurationElementCollection(ConfigurationElementCollectionType collectionType, string elementName, Func<T, object> getElementKeyFunc)
        {
            _collectionType = collectionType;
            _elementName = elementName ?? throw new ArgumentNullException(nameof(elementName));
            _getElementKeyFunc = getElementKeyFunc ?? throw new ArgumentNullException(nameof(getElementKeyFunc));
        }

        protected sealed override object GetElementKey(T element) => _getElementKeyFunc(element);

        public sealed override ConfigurationElementCollectionType CollectionType => _collectionType;
        protected override string ElementName => _elementName;

        T IConfigurationElementCollection<T>.this[int index] => this[index];
    }
}
#endif