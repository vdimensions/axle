using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Documents.Binding
{
    internal sealed class DocumentValueProvider : IDocumentComplexValueProvider
    {
        private sealed class DocumentComplexValueProvider : IDocumentComplexValueProvider
        {
            private readonly ITextDocumentObject _documentNode;

            public DocumentComplexValueProvider(ITextDocumentObject documentNode)
            {
                _documentNode = documentNode;
            }

            public bool TryGetValue(string member, out IDocumentValueProvider value)
            {
                var providers = _documentNode.GetChildren(member).Select(GetProvider).ToArray();
                value = providers.Length == 1 
                    ? providers[0] 
                    : new DocumentCollectionValueProvider(member, _documentNode.GetChildren(member).Select(GetProvider));
                return true;
            }

            public string Name => _documentNode.Key;

            public IDocumentValueProvider this[string member] => TryGetValue(member, out var value) ? value : null;
        }

        private sealed class DocumentCollectionValueProvider : IDocumentCollectionProvider
        {
            private readonly IEnumerable<IDocumentValueProvider> _providers;

            public DocumentCollectionValueProvider(string name, IEnumerable<IDocumentValueProvider> providers)
            {
                _providers = providers;
                Name = name;
            }

            public IEnumerator<IDocumentValueProvider> GetEnumerator() => _providers.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public string Name { get; }

            IDocumentCollectionValueAdapter IDocumentCollectionProvider.CollectionValueAdapter => null;
        }

        private static IDocumentValueProvider GetProvider(ITextDocumentNode node)
        {
            switch (node)
            {
                case ITextDocumentValue v:
                    return new DocumentSimpleValueProvider(v.Key, v.Value);
                case ITextDocumentObject o:
                    return new DocumentComplexValueProvider(o);
            }
            throw new NotSupportedException($"Unsupported node type {node.GetType().FullName}");
        }

        private readonly DocumentComplexValueProvider _inner;

        public DocumentValueProvider(ITextDocumentRoot structuredDocument) { _inner = (DocumentComplexValueProvider) GetProvider(structuredDocument); }

        public bool TryGetValue(string member, out IDocumentValueProvider value) => _inner.TryGetValue(member, out value);

        public string Name => _inner.Name;

        public IDocumentValueProvider this[string member] => _inner[member];
    }
}
