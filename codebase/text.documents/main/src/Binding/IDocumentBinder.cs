using System;

namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// An interface representing a binder; that is an object which is used to bind a structured data format to
    /// an object instance.
    /// </summary>
    public interface IDocumentBinder
    {
        /// <summary>
        /// Binds the provided <paramref name="instance"/> object with the data available in the given 
        /// <paramref name="memberValueProvider"/>.
        /// </summary>
        /// <param name="memberValueProvider">
        /// The <see cref="IDocumentValueProvider"/> instance that is supplying values for the binding process.
        /// </param>
        /// <param name="instance">
        /// An <see cref="object"/> instance that will be updated with the provided by the 
        /// <paramref name="memberValueProvider"/> values.
        /// </param>
        /// <returns>
        /// A reference to the data-bound object.
        /// </returns>
        object Bind(IDocumentValueProvider memberValueProvider, object instance);
        /// <summary>
        /// Binds an object instance of the provided <paramref name="type"/> with the data available in the given 
        /// <paramref name="memberValueProvider"/>.
        /// </summary>
        /// <param name="memberValueProvider">
        /// The <see cref="IDocumentValueProvider"/> instance that is supplying values for the binding process.
        /// </param>
        /// <param name="type">
        /// The <see cref="Type"/> of the object instance that will be updated with the provided by the 
        /// <paramref name="memberValueProvider"/> values.
        /// </param>
        /// <returns>
        /// A reference to the data-bound object.
        /// </returns>
        object Bind(IDocumentValueProvider memberValueProvider, Type type);
    }
}
