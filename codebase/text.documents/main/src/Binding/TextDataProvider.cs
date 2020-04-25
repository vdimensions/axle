using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Documents.Binding
{
    internal sealed class TextDataProvider : IBoundComplexValueProvider
    {
        private sealed class BCVP : IBoundComplexValueProvider
        {
            private readonly ITextDocumentObject _provider;

            public BCVP(ITextDocumentObject provider)
            {
                _provider = provider;
            }

            public bool TryGetValue(string member, out IBoundValueProvider value)
            {
                value = new BCP(member, _provider.GetChildren(member).Select(GetProvider));
                return true;
            }

            public string Name => _provider.Key;

            public IBoundValueProvider this[string member] => TryGetValue(member, out var value) ? value : null;
        }

        private sealed class BCP : IBoundCollectionProvider
        {
            private readonly IEnumerable<IBoundValueProvider> _providers;

            public BCP(string name, IEnumerable<IBoundValueProvider> providers)
            {
                _providers = providers;
                Name = name;
            }

            public IEnumerator<IBoundValueProvider> GetEnumerator() => _providers.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public string Name { get; }

            IBindingCollectionAdapter IBoundCollectionProvider.CollectionAdapter => null;
        }

        private static IBoundValueProvider GetProvider(ITextDocumentNode node)
        {
            switch (node)
            {
                case ITextDocumentValue v:
                    return new BoundSimpleValueProvider(v.Key, v.Value);
                case ITextDocumentObject o:
                    return new BCVP(o);
            }
            throw new NotSupportedException($"Unsupported node type {node.GetType().FullName}");
        }

        private readonly BCVP _inner;

        public TextDataProvider(ITextDocumentRoot structuredDocument) { _inner = (BCVP) GetProvider(structuredDocument); }

        public bool TryGetValue(string member, out IBoundValueProvider value) => _inner.TryGetValue(member, out value);

        public string Name => _inner.Name;

        public IBoundValueProvider this[string member] => _inner[member];
    }
}
