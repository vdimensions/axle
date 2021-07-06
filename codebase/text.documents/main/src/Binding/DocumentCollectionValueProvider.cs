using System.Collections;
using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    internal sealed class DocumentCollectionValueProvider : IDocumentCollectionValueProvider
    {
        private readonly IEnumerable<IDocumentValueProvider> _providers;

        public DocumentCollectionValueProvider(string name, IDocumentCollectionValueAdapter valueAdapter, params IDocumentValueProvider[] providers)
        {
            Name = name;
            ValueAdapter = valueAdapter;
            _providers = providers;
        }
        public DocumentCollectionValueProvider(string name, params IDocumentValueProvider[] providers) : this(name, null, providers) { }
        public DocumentCollectionValueProvider(string name) : this(name, null, new IDocumentValueProvider[0]) { }

        public IEnumerator<IDocumentValueProvider> GetEnumerator() => _providers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string Name { get; }
        public IDocumentCollectionValueAdapter ValueAdapter { get; }
    }
}