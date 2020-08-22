namespace Axle.Text.Documents
{
    public interface ITextDocumentValue : ITextDocumentNode
    {
        CharSequence Value { get; }
    }
}
