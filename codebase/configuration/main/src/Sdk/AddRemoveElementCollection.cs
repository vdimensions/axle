using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;


namespace Axle.Configuration.Sdk
{
    public abstract class AbstractAddRemoveElementCollection<TAdd, TRemove, T> : AbstractConfigurationElementCollection<T>, IAddRemoveElementCollection<TAdd, TRemove>
        where TAdd: T, new()
        where TRemove: T, new()
        where T: ConfigurationElement
    {
        private struct ElementKey : IEquatable<ElementKey>
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly object _originalElementKey;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly AddRemoveOperation _operation;

            public ElementKey(Object originalElementKey, AddRemoveOperation operation) : this()
            {
                _originalElementKey = originalElementKey ?? throw new ArgumentNullException(nameof(originalElementKey));
                _operation = operation;
            }

            public override bool Equals(object obj) { return obj != null && obj is ElementKey && DoEquals((ElementKey) obj); }
            public bool Equals(ElementKey other) { return DoEquals(other); }

            public override int GetHashCode() { return _originalElementKey.GetHashCode()^_operation.GetHashCode(); }

            #if net45
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            #endif
            private bool DoEquals(ElementKey other) { return _operation == other._operation && Equals(_originalElementKey, other._originalElementKey); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IConfigurationElementParser<TAdd> addElementParser;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IConfigurationElementParser<TRemove> removeElementParser;

        protected AbstractAddRemoveElementCollection()
        {
            RegisterParser(addElementParser = CreateAddElementParser());
            RegisterParser(removeElementParser = CreateRemoveElementParser());
        }

        protected sealed override ConfigurationElement CreateNewElement(string elementName)
        {
            if (addElementParser.Accept(elementName))
            {
                return CreateAddElement();
            }
            if (removeElementParser.Accept(elementName))
            {
                return CreateRemoveElement();
            }
            throw new ArgumentException("Unsupported element '" + elementName + "'", "elementName");
        }

        protected virtual TAdd CreateAddElement() { return new TAdd(); }

        protected virtual TRemove CreateRemoveElement() { return new TRemove(); }

        protected virtual IConfigurationElementParser<TAdd> CreateAddElementParser() { return new AddElementParser<TAdd>(); }

        protected virtual IConfigurationElementParser<TRemove> CreateRemoveElementParser() { return new RemoveElementParser<TRemove>(); }

        protected sealed override object GetElementKey(T element)
        {
            object elementKey = null;
            AddRemoveOperation operation;
            switch (element)
            {
                case TAdd add:
                    elementKey = GetElementKey(add);
                    operation = AddRemoveOperation.Add;
                    break;
                case TRemove remove:
                    elementKey = GetElementKey(remove);
                    operation = AddRemoveOperation.Remove;
                    break;
                default:
                    throw new InvalidOperationException("Unsupported element type " + element.GetType());
            }
            return new ElementKey(elementKey, operation);
        }
        protected abstract object GetElementKey(TAdd element);
        protected abstract object GetElementKey(TRemove element);

        protected override string ElementName => string.Empty;

        public IEnumerable<TAdd> AddedElements => this.OfType<TAdd>();
        public IEnumerable<TRemove> RemovedElements => this.OfType<TRemove>();
    }
}