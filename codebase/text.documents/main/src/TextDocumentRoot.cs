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
        private readonly IEqualityComparer<string> _comparer;
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly TextDocumentObject _inner;

        private TextDocumentRoot(TextDocumentObject inner, IEqualityComparer<string> comparer)
        {
            _inner = inner;
            _comparer = comparer;
        }
        public TextDocumentRoot(IEnumerable<ITextDocumentNode> children, IEqualityComparer<string> comparer)
            : this(new TextDocumentObject(String.Empty, null, children, true), comparer) { }

        public IEnumerable<ITextDocumentNode> GetChildren() => _inner.GetChildren();
        public IEnumerable<ITextDocumentNode> GetChildren(string name) => _inner.GetChildren(name);

        public ITextDocumentObject Parent => _inner.Parent;
        public string Key => _inner.Key;
        IEqualityComparer<string> ITextDocumentNode.KeyComparer => _comparer;
    }
}
