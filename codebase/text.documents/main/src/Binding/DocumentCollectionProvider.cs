using System.Collections;
using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    internal sealed class DocumentCollectionProvider : IDocumentCollectionProvider
    {
        private readonly IEnumerable<IDocumentValueProvider> _providers;

        public DocumentCollectionProvider(string name, IDocumentCollectionValueAdapter collectionValueAdapter, params IDocumentValueProvider[] providers)
        {
            Name = name;
            CollectionValueAdapter = collectionValueAdapter;
            _providers = providers;
        }
        public DocumentCollectionProvider(string name, params IDocumentValueProvider[] providers)  : this(name, null, providers){ }

        public IEnumerator<IDocumentValueProvider> GetEnumerator() => _providers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string Name { get; }
        public IDocumentCollectionValueAdapter CollectionValueAdapter { get; }
    }
}