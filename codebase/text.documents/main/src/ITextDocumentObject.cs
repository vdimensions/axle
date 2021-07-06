using System.Collections.Generic;

namespace Axle.Text.Documents
{
    /// <summary>
    /// An interface for a node in a text document that represents a compound object.
    /// </summary>
    /// <seealso cref="ITextDocumentNode"/>
    public interface ITextDocumentObject : ITextDocumentNode
    {
        /// <summary>
        /// Gets a collection of <see cref="ITextDocumentNode">nodes</see> which are the logical children of the current
        /// <see cref="ITextDocumentObject"/> instance. 
        /// </summary>
        /// <returns>
        /// A collection of <see cref="ITextDocumentNode">nodes</see> which are the logical children of the current
        /// <see cref="ITextDocumentObject"/> instance. 
        /// </returns>
        IEnumerable<ITextDocumentNode> GetChildren();
        
        /// <summary>
        /// Gets a collection of <see cref="ITextDocumentNode">nodes</see> which represents a document node that
        /// is a collection of values and has the specified <paramref name="name"/>.
        /// <para>
        /// The <see cref="ITextDocumentNode.KeyComparer"/> property is used to group together the matching nodes.
        /// </para>
        /// </summary>
        /// <returns>
        /// A collection of <see cref="ITextDocumentNode">nodes</see> which represents a document node that
        /// is a collection of values and has the specified <paramref name="name"/>.
        /// </returns>
        IEnumerable<ITextDocumentNode> GetValues(string name);
    }
}
