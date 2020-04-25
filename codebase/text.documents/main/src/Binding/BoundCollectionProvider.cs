using System.Collections;
using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    internal sealed class BoundCollectionProvider : IBoundCollectionProvider
    {
        private readonly IEnumerable<IBoundValueProvider> _providers;

        public BoundCollectionProvider(string name, IBindingCollectionAdapter collectionAdapter, params IBoundValueProvider[] providers)
        {
            Name = name;
            CollectionAdapter = collectionAdapter;
            _providers = providers;
        }
        public BoundCollectionProvider(string name, params IBoundValueProvider[] providers)  : this(name, null, providers){ }

        public IEnumerator<IBoundValueProvider> GetEnumerator() => _providers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string Name { get; }
        public IBindingCollectionAdapter CollectionAdapter { get; }
    }
}