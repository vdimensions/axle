using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Documents
{
    public sealed class TextDocumentSubset : ITextDocumentRoot
    {
        private readonly ITextDocumentObject _textDocument;
        
        public TextDocumentSubset(ITextDocumentObject textDocument, string prefix)
        {
            _textDocument = textDocument;
            Prefix = prefix;
        }
        public IEnumerable<ITextDocumentNode> GetChildren()
        {
            return _textDocument.GetValues(Prefix);
        }

        public IEnumerable<ITextDocumentNode> GetValues(string name) => 
            GetChildren().OfType<ITextDocumentObject>().SelectMany(x => x.GetValues(name));

        public IEqualityComparer<string> KeyComparer => _textDocument.KeyComparer;
        public ITextDocumentObject Parent => null;
        public string Prefix { get; }
        public string Key => Prefix;
    }
}