using System;
using System.Collections.Generic;

namespace Axle.Text.Data
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class TextDataObjectRoot : ITextDataRoot
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly StringComparer _comparer;
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly TextDataObject _inner;

        private TextDataObjectRoot(TextDataObject inner, StringComparer comparer)
        {
            _inner = inner;
            _comparer = comparer;
        }
        public TextDataObjectRoot(IEnumerable<ITextDataNode> children, StringComparer comparer)
            : this(new TextDataObject(String.Empty, null, children, true), comparer) { }

        public IEnumerable<ITextDataNode> GetChildren() => _inner.GetChildren();
        public IEnumerable<ITextDataNode> GetChildren(string name) => _inner.GetChildren(name);

        public ITextDataObject Parent => _inner.Parent;
        public string Key => _inner.Key;
        StringComparer ITextDataNode.KeyComparer => _comparer;
    }
}
