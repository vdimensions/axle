namespace Axle.Text.Documents
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class TextDocumentValue : TextDocumentNode, ITextDocumentValue
    {
        public TextDocumentValue(string key, ITextDocumentObject parent, string value) : base(key, parent)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
