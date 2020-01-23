using System;

namespace Axle.Text.Data
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class TextDataValue : TextDataNode, ITextDataValue
    {
        public TextDataValue(string key, ITextDataObject parent, string value) : base(key, parent)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
