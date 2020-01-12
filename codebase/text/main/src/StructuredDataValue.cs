using System;

namespace Axle.Text.StructuredData
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class StructuredDataValue : StructuredDataNode, IStructuredDataValue
    {
        public StructuredDataValue(string key, IStructuredDataObject parent, string value) : base(key, parent)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
