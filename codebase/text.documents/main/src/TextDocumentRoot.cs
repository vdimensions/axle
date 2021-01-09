using System;
using System.Collections.Generic;

namespace Axle.Text.Documents
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    internal sealed class TextDocumentRoot : ITextDocumentRoot
    {
        #if NETSTANDARD2_0_OR_NEWER || NET30_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly IEqualityComparer<string> _comparer;
        
        #if NETSTANDARD2_0_OR_NEWER || NET30_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly TextDocumentObject _inner;

        private TextDocumentRoot(TextDocumentObject inner, IEqualityComparer<string> comparer)
        {
            _inner = inner;
            _comparer = comparer;
        }
        internal TextDocumentRoot(IEnumerable<ITextDocumentNode> children, IEqualityComparer<string> comparer)
            : this(new TextDocumentObject(string.Empty, null, children, true), comparer) { }

        public IEnumerable<ITextDocumentNode> GetChildren() => _inner.GetChildren();
        public IEnumerable<ITextDocumentNode> GetValues(string name) => _inner.GetValues(name);

        public ITextDocumentObject Parent => _inner.Parent;
        public string Key => _inner.Key;
        IEqualityComparer<string> ITextDocumentNode.KeyComparer => _comparer;
    }
}
