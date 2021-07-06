using Axle.Configuration;

namespace Axle.Application
{
    /// <summary>
    /// An interface that enables an axle application to use specific configuration sources programmatically.
    /// </summary>
    public interface IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Adds the provided <paramref name="configSource"/> to the list of the application configuration sources.
        /// The configuration data represented by the provided source will be loaded and used by the application.
        /// </summary>
        /// <param name="configSource">
        /// The <see cref="IConfigSource"/> representing the additional application configuration to load.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IApplicationConfigurationBuilder"/> instance.
        /// </returns>
        IApplicationConfigurationBuilder Add(IConfigSource configSource);
    }
}