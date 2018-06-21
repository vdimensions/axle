using System;


namespace Axle.Core.DependencyInjection
{
    /// <summary>
    /// An interface representing a dependency container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers the provided object <paramref name="instance" /> within the current <see cref="IContainer">container</see>.
        /// </summary>
        /// <param name="instance">
        /// The object instance to be registered.
        /// </param>
        /// <param name="name">
        /// The name of the context within which the <paramref name="instance" /> will be registered.
        /// </param>
        /// <param name="aliases">
        /// An optional list of additional context names to register the <paramref name="instance" />.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IContainer">container</see>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="instance" /> or <paramref name="name" /> is <c>null</c>.
        /// </exception>
        IContainer RegisterInstance(object instance, string name, params string[] aliases);

        /// <summary>
        /// Registers the provided <paramref name="type" /> within the current <see cref="IContainer">container</see>.
        /// </summary>
        /// <param name="type">
        /// The type of the dependency to be registered.
        /// </param>
        /// <param name="name">
        /// The name of the context within which the dependency will be registered.
        /// </param>
        /// <param name="aliases">
        /// An optional list of additional context names to register the dependency.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IContainer">container</see>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="type" /> or <paramref name="name" /> is <c>null</c>.
        /// </exception>
        IContainer RegisterType(Type type, string name, params string[] aliases);

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

        /// <summary>
        /// Gets a reference to the parent <see cref="IContainer"/> of the dependency container hierarchy.
        /// In case the current instance is the root of the hierarchy, the value returned will be <c>null</c>.
        /// </summary>
        IContainer Parent { get; }
    }
}
