using System;

namespace Axle.DependencyInjection
{
    public interface IDependencyRegistry
    {
        /// <summary>
        /// Registers the provided <paramref name="type" /> within the current <see cref="IDependencyContainer">container</see>.
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
        /// A reference to the current <see cref="IDependencyContainer">container</see>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="type" /> or <paramref name="name" /> is <c>null</c>.
        /// </exception>
        IDependencyRegistry RegisterType(Type type, string name, params string[] aliases);
    }
}