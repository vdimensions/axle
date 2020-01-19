using System.Collections;
using System.Collections.Generic;

namespace Axle.Text.StructuredData.Binding
{
    internal sealed class CollectionValueProvider : ICollectionMemberValueProvider
    {
        private readonly IEnumerable<IBindingValueProvider> _providers;

        public CollectionValueProvider(string name, IBindingCollectionAdapter collectionAdapter, params IBindingValueProvider[] providers)
        {
            Name = name;
            CollectionAdapter = collectionAdapter;
            _providers = providers;
        }
        public CollectionValueProvider(string name, params IBindingValueProvider[] providers)  : this(name, null, providers){ }

        public IEnumerator<IBindingValueProvider> GetEnumerator() => _providers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string Name { get; }
        public IBindingCollectionAdapter CollectionAdapter { get; }
    }
}