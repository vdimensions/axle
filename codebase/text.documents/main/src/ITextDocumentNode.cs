using System.Collections.Generic;

namespace Axle.Text.Documents
{
    /// <summary>
    /// An interface representing a node of a text document structure.
    /// </summary>
    public interface ITextDocumentNode
    {
        /// <summary>
        /// An <see cref="ITextDocumentObject"/> instance, that is the logical parent of the current
        /// <see cref="ITextDocumentNode"/>, or <c>null</c> if the current <see cref="ITextDocumentNode"/> instance is
        /// the root node of the text document structure it represents.
        /// </summary>
        ITextDocumentObject Parent { get; }
        
        /// <summary>
        /// A <see cref="string"/> value representing the unique key identifying a particular
        /// <see cref="ITextDocumentNode"/>.
        /// </summary>
        string Key { get; }
        
        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> instance that is used to compare the keys of a
        /// <see cref="ITextDocumentNode"/> when listing sibling nodes.
        /// </summary>
        IEqualityComparer<string> KeyComparer { get; }
    }
}
