using System.Collections.Generic;

namespace Axle.Text.Documents
{
    /// <summary>
    /// An interface representing a text document adapter. The <see cref="ITextDocumentAdapter"/> exposes the necessary
    /// interface for interpreting the contents of a text document in order to produce <see cref="ITextDocumentNode"/>.
    /// </summary>
    public interface ITextDocumentAdapter
    {
        /// <summary>
        /// The key that identifies the current <see cref="ITextDocumentNode"/> represented by the current
        /// <see cref="ITextDocumentAdapter"/> instance.
        /// </summary>
        string Key { get; }
        
        /// <summary>
        /// A <see cref="CharSequence"/> representing the value of the currently represented
        /// <see cref="ITextDocumentNode"/>, or <c>null</c> if the current node is a complex value consisting of
        /// multiple nested <see cref="ITextDocumentNode"/> items.
        /// </summary>
        CharSequence Value { get; }
        
        /// <summary>
        /// A collection of <see cref="ITextDocumentAdapter"/> instances, each representing a
        /// <see cref="ITextDocumentNode"/> that is a logical child of the <see cref="ITextDocumentNode"/> represented
        /// by the current <see cref="ITextDocumentAdapter"/> instance.
        /// </summary>
        IEnumerable<ITextDocumentAdapter> Children { get; }
    }
}