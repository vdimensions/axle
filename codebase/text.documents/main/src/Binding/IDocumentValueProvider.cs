namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// An interface representing a document value provider; that is an object which provides values for 
    /// object members to a <see cref="IDocumentBinder"/> during the binding process.
    /// </summary>
    /// <seealso cref="IDocumentBinder"/>
    public interface IDocumentValueProvider
    {
        /// <summary>
        /// The name of the member this <see cref="IDocumentValueProvider"/> is providing values for.
        /// </summary>
        /// <remarks>
        /// An <see cref="string.Empty">empty string</see> (<c>""</c>) value is allowed when the current 
        /// <see cref="IDocumentValueProvider"/> addresses the root object being bound.
        /// </remarks>
        string Name { get; }
    }
}
