namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// The default implementation of the <see cref="IDocumentSimpleValueProvider"/> interface.
    /// </summary>
    public sealed class DocumentSimpleValueProvider : IDocumentSimpleValueProvider
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DocumentSimpleValueProvider"/> class.
        /// </summary>
        /// <param name="name">The name of the represented member.</param>
        /// <param name="value">The raw value of the represented member.</param>
        public DocumentSimpleValueProvider(string name, CharSequence value)
        {
            Name = name;
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public CharSequence Value { get; }
    }
}