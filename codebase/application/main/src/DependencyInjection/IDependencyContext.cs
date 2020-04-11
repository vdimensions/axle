using System;

namespace Axle.DependencyInjection
{
    public interface IDependencyContext
    {
        /// <summary>
        /// Resolves a dependency object using the requested <paramref name="type"/> and <paramref name="name"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the dependency resolve.
        /// </param>
        /// <param name="name">
        /// The name of the dependency to resolve.
        /// </param>
        /// <returns>
        /// An object instance representing the dependency.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="type"/> or <paramref name="name"/> is <c>null</c>.
        /// </exception>
        object Resolve(Type type, string name);
        
        IDependencyContext Parent { get; }
    }
}