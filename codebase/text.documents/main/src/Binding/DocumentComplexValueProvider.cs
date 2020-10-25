using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Documents.Binding
{
    public sealed class DocumentComplexValueProvider : IDocumentComplexValueProvider
    {
        private readonly ITextDocumentObject _documentNode;

        internal DocumentComplexValueProvider(ITextDocumentObject documentNode)
        {
            _documentNode = documentNode;
        }

        public bool TryGetValue(string member, out IDocumentValueProvider value)
        {
            var providers = _documentNode.GetValues(member).Select(DocumentValueProvider.Get).ToArray();
            value = providers.Length == 1 
                ? providers[0] 
                : new DocumentCollectionValueProvider(member, providers);
            return true;
        }

        public IEnumerable<IDocumentValueProvider> GetChildren() => _documentNode.GetChildren().Select(DocumentValueProvider.Get).ToArray();

        public string Name => _documentNode.Key;

        public IDocumentValueProvider this[string member] => TryGetValue(member, out var value) ? value : null;
        
    }
}