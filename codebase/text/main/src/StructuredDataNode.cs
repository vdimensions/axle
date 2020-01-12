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
        private readonly string _name;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly IStructuredDataObject _parent;

        protected StructuredDataNode(string name, IStructuredDataObject parent)
        {
            _parent = parent;
            _name = name;
        }

        StringComparer IStructuredDataNode.KeyComparer => Parent?.KeyComparer ?? StringComparer.OrdinalIgnoreCase;
        public string Name => _name;
        public IStructuredDataObject Parent => _parent;
    }
}