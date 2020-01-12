using System;

namespace Axle.Text.StructuredData
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class StructuredDataValue : StructuredDataNode, IStructuredDataValue
    {
        public StructuredDataValue(string name, IStructuredDataObject parent, string value) : base(name, parent)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
