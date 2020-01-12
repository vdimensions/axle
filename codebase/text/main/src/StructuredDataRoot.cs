using System;
using System.Collections.Generic;

namespace Axle.Text.StructuredData
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class StructuredDataObjectRoot : IStructuredDataRoot
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly StringComparer _comparer;
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly StructuredDataObject _inner;

        private StructuredDataObjectRoot(StructuredDataObject inner, StringComparer comparer)
        {
            _inner = inner;
            _comparer = comparer;
        }
        public StructuredDataObjectRoot(IEnumerable<IStructuredDataNode> children, StringComparer comparer)
            : this(new StructuredDataObject(String.Empty, null, children, true), comparer) { }

        public IEnumerable<IStructuredDataNode> GetChildren() => _inner.GetChildren();
        public IEnumerable<IStructuredDataNode> GetChildren(string name) => _inner.GetChildren(name);

        public string Key => _inner.Key;
        public IStructuredDataObject Parent => _inner.Parent;
        StringComparer IStructuredDataNode.KeyComparer => _comparer;
    }
}
