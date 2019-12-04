using System;

namespace Axle.Conversion.Binding
{
    /// <summary>
    /// An interface representing a binder; that is an object which is used to bind a structured data format to
    /// an object instance.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Binds the provided <paramref name="instance"/> object with the data available in the given 
        /// <paramref name="bindingContext"/>.
        /// </summary>
        /// <param name="bindingContext">
        /// The <see cref="BindingContext"/> instance that is supplying context information for the binding process.
        /// </param>
        /// <param name="instance">
        /// An <see cref="object"/> instance that will be updated with the provided by the 
        /// <paramref name="bindingContext"/> values.
        /// </param>
        /// <returns>
        /// A refernce to the databound object.
        /// </returns>
        object Bind(BindingContext bindingContext, object instance);
        /// <summary>
        /// Binds an object instance of the provided <paramref name="type"/> with the data available in the given 
        /// <paramref name="bindingContext"/>.
        /// </summary>
        /// <param name="bindingContext">
        /// The <see cref="BindingContext"/> instance that is supplying context information for the binding process.
        /// </param>
        /// <param name="type">
        /// The <see cref="Type"/> of the object instance that will be updated with the provided by the 
        /// <paramref name="bindingContext"/> values.
        /// </param>
        /// <returns>
        /// A refernce to the databound object.
        /// </returns>
        object Bind(BindingContext bindingContext, Type type);
    }
}
