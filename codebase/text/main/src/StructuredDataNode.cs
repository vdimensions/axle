using System;

namespace Axle.Text.StructuredData
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class StructuredDataNode : IStructuredDataNode
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly string _key;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly IStructuredDataObject _parent;

        protected StructuredDataNode(string key, IStructuredDataObject parent)
        {
            _parent = parent;
            _key = key;
        }

        StringComparer IStructuredDataNode.KeyComparer => Parent?.KeyComparer ?? StringComparer.OrdinalIgnoreCase;
        public string Key => _key;
        public IStructuredDataObject Parent => _parent;
    }
}