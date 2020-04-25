namespace Axle.Configuration
{
    /// <summary>
    /// An interface providing abstraction over a configuration source; that is, an object which is capable of
    /// loading a <see cref="IConfigSection"/> instance.
    /// </summary>
    public interface IConfigSource
    {
        /// <summary>
        /// Loads the <see cref="IConfigSection"/> instance from the origins represented by the current
        /// <see cref="IConfigSource"/> implementation.
        /// </summary>
        /// <returns>
        /// An <see cref="IConfigSection"/> instance representing the loaded configuration.
        /// </returns>
        IConfiguration LoadConfiguration();
    }
}