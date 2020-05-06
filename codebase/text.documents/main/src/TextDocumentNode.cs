using System;
using System.Collections.Generic;

namespace Axle.Text.Documents
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class TextDocumentNode : ITextDocumentNode
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly string _key;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly ITextDocumentObject _parent;

        protected TextDocumentNode(string key, ITextDocumentObject parent)
        {
            _parent = parent;
            _key = key;
        }

        IEqualityComparer<string> ITextDocumentNode.KeyComparer => Parent?.KeyComparer ?? StringComparer.OrdinalIgnoreCase;
        public string Key => _key;
        public ITextDocumentObject Parent => _parent;
    }
}