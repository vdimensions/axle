namespace Axle.Text.Documents
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class TextDocumentValue : TextDocumentNode, ITextDocumentValue
    {
        internal TextDocumentValue(string key, ITextDocumentObject parent, string value) : base(key, parent)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
