using System;

namespace Axle.Text.Data
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class TextDataNode : ITextDataNode
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly string _key;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly ITextDataObject _parent;

        protected TextDataNode(string key, ITextDataObject parent)
        {
            _parent = parent;
            _key = key;
        }

        StringComparer ITextDataNode.KeyComparer => Parent?.KeyComparer ?? StringComparer.OrdinalIgnoreCase;
        public string Key => _key;
        public ITextDataObject Parent => _parent;
    }
}