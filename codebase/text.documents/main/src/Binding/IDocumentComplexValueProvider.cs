using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// An interface representing an binder object provider; that is a type of <see cref="IDocumentValueProvider"/> 
    /// that is used to supply values for complex types.
    /// </summary>
    public interface IDocumentComplexValueProvider : IDocumentValueProvider
    {
        /// <summary>
        /// Attempts to get the value(s) associated for the given <paramref name="member"/>.
        /// </summary>
        /// <param name="member">
        /// The member to lookup values for.
        /// </param>
        /// <param name="value">
        /// An <see cref="IDocumentValueProvider"/> instance representing the value associated with the given 
        /// <paramref name="member"/>. 
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c>, if there are values for the given <paramref name="member"/>; <c>false</c> otherwise.
        /// </returns>
        bool TryGetValue(string member, out IDocumentValueProvider value);

        /// <summary>
        /// Gets a collection of values representing the logical children of the node represented by the current
        /// <see cref="IDocumentComplexValueProvider"/> instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDocumentValueProvider> GetChildren();

        /// <summary>
        /// Gets the value(s) associated for the given <paramref name="member"/>.
        /// </summary>
        /// <param name="member">
        /// The member to lookup values for.
        /// </param>
        /// <returns>
        /// An <see cref="IDocumentValueProvider"/> instance representing the values associated with the given 
        /// <paramref name="member"/>, or <c><see langword="null"/></c> if no values exist for the given member.
        /// </returns>
        IDocumentValueProvider this[string member] { get; }
    }
}
