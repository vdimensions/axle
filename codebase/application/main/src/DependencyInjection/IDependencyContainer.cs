using System;


namespace Axle.DependencyInjection
{
    /// <summary>
    /// An interface representing a dependency container.
    /// </summary>
    public interface IDependencyContainer : IDependencyExporter, IDependencyRegistry, IDependencyContext, IDisposable
    {
        /// <summary>
        /// Registers the provided object <paramref name="instance" /> within the current <see cref="IDependencyContainer">container</see>.
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
        /// A reference to the current <see cref="IDependencyContainer">container</see>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="instance" /> or <paramref name="name" /> is <c>null</c>.
        /// </exception>
        IDependencyContainer RegisterInstance(object instance, string name, params string[] aliases);
    }
}
