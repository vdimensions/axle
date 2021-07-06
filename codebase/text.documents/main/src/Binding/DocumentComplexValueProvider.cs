using System;
using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Documents.Binding
{
    public sealed class DocumentComplexValueProvider : IDocumentComplexValueProvider
    {
        private static readonly Func<ITextDocumentNode, IDocumentValueProvider> ResolveDocumentValueProvider = DocumentValueProvider.Get;
        private static readonly Func<IDocumentValueProvider, bool> DocumentValueProviderNotNull = x => x != null;
        
        private readonly ITextDocumentObject _documentNode;

        internal DocumentComplexValueProvider(ITextDocumentObject documentNode)
        {
            _documentNode = documentNode;
        }

        public bool TryGetValue(string member, out IDocumentValueProvider value)
        {
            var providers = _documentNode
                .GetValues(member)
                .Select(ResolveDocumentValueProvider)
                .Where(DocumentValueProviderNotNull)
                .ToArray();
            value = providers.Length == 1 
                ? providers[0] 
                : new DocumentCollectionValueProvider(member, providers);
            return providers.Length != 0;
        }

        public IEnumerable<IDocumentValueProvider> GetChildren()
        {
            return _documentNode.GetChildren()
                .Select(ResolveDocumentValueProvider)
                .Where(DocumentValueProviderNotNull)
                .ToArray();
        }

        public string Name => _documentNode.Key;

        public IDocumentValueProvider this[string member] => TryGetValue(member, out var value) ? value : null;
        
    }
}