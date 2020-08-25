namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// An interface representing a <see cref="IDocumentSimpleValueProvider" />, that is a document binder value
    /// provider used to supply simple data types.
    /// </summary>
    public interface IDocumentSimpleValueProvider : IDocumentValueProvider
    {
        /// <summary>
        /// A string representation of the value represented by the current <see cref="IDocumentSimpleValueProvider"/>   
        /// implementation.
        /// </summary>
        CharSequence Value { get; }
    }
}
