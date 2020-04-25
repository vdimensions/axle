using System;
using System.Collections.Generic;

namespace Axle.Text.Documents
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class TextDocumentRoot : ITextDocumentRoot
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly StringComparer _comparer;
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly TextDocumentObject _inner;

        private TextDocumentRoot(TextDocumentObject inner, StringComparer comparer)
        {
            _inner = inner;
            _comparer = comparer;
        }
        public TextDocumentRoot(IEnumerable<ITextDocumentNode> children, StringComparer comparer)
            : this(new TextDocumentObject(String.Empty, null, children, true), comparer) { }

        public IEnumerable<ITextDocumentNode> GetChildren() => _inner.GetChildren();
        public IEnumerable<ITextDocumentNode> GetChildren(string name) => _inner.GetChildren(name);

        public ITextDocumentObject Parent => _inner.Parent;
        public string Key => _inner.Key;
        StringComparer ITextDocumentNode.KeyComparer => _comparer;
    }
}
