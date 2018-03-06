using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Xml;

using Axle.Verification;


namespace Axle.Configuration.Sdk
{
    public abstract class AbstractConfigurationElementCollection : ConfigurationElementCollection, ISupportsDeserializeInternal
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LinkedList<IConfigurationElementParser> _parsers = new LinkedList<IConfigurationElementParser>();

        protected AbstractConfigurationElementCollection()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitElementParsers();
            RegisterProperties(Properties);
        }

        protected virtual void RegisterProperties(ConfigurationPropertyCollection properties) { }
        protected virtual void InitElementParsers() { }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            var parser = _parsers.SingleOrDefault(x => x.Accept(elementName));
            if (parser == null)
            {
                return base.OnDeserializeUnrecognizedElement(elementName, reader);
            }
            var parsedElement = parser.Parse(reader);
            BaseAdd(parsedElement);
            return true;
        }

        protected void RegisterParser(IConfigurationElementParser parser)
        {
            _parsers.AddFirst(parser);
        }

        protected T Resolve<T>(ConfigurationProperty property)
        {
            return (T) base[property.VerifyArgument("property").IsNotNull().Value];
        }

        void ISupportsDeserializeInternal.Deserialize(XmlReader reader, bool serializeCollectionKey) { DeserializeElement(reader, serializeCollectionKey); }

        protected sealed override ConfigurationPropertyCollection Properties => _configurationProperties;
        public abstract override ConfigurationElementCollectionType CollectionType { get; }
        protected abstract override string ElementName { get; }
    }

    public abstract class AbstractConfigurationElementCollection<TElement> : AbstractConfigurationElementCollection, IEnumerable<TElement>
        where TElement: ConfigurationElement
    {
        protected sealed override void BaseAdd(ConfigurationElement element) { this.BaseAdd((TElement) element); }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        protected void BaseAdd(TElement element) { base.BaseAdd(element); }
        protected sealed override void BaseAdd(int index, ConfigurationElement element)
        {
            this.BaseAdd(index, (TElement) element);
        }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        protected void BaseAdd(int index, TElement element) { base.BaseAdd(index, element); }

        new protected TElement BaseGet(int index) { return (TElement) base.BaseGet(index); }
        new protected TElement BaseGet(object key) { return (TElement) base.BaseGet(key); }

        protected sealed override ConfigurationElement CreateNewElement() { return CreateNewSpecificElement(); }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        protected virtual TElement CreateNewSpecificElement()
        {
            return typeof (TElement).IsAbstract ? null : (TElement) Activator.CreateInstance(typeof(TElement), false);
        }

        protected sealed override object GetElementKey(ConfigurationElement element)
        {
            return GetElementKey((TElement) element);
        }
        protected abstract object GetElementKey(TElement element);

        new public IEnumerator<TElement> GetEnumerator() { return DoGetEnumerator(); }
        IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator() { return DoGetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return base.GetEnumerator(); }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private IEnumerator<TElement> DoGetEnumerator()
        {
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current as TElement;
            }
        }

        public bool TryGetValue(int index, out TElement value)
        {
            value = BaseGet(index);
            return value != null;
        }

        public TElement this[int index]
        {
            get => BaseGet(index);
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        public TElement this[object key] => BaseGet(key);
        public new object this[ConfigurationProperty key] => base[key];
    }

    public abstract class AbstractConfigurationElementCollection<TKey, TElement> : AbstractConfigurationElementCollection<TElement> where TElement: ConfigurationElement
    {
        protected TElement BaseGet(TKey key) => base.BaseGet(key);

        public bool TryGetValue(TKey index, out TElement value)
        {
            value = BaseGet(index);
            return value != null;
        }

        public TElement this[TKey key] => BaseGet(key);
    }
}