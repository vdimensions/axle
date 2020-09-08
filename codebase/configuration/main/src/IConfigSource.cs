namespace Axle.Configuration
{
    /// <summary>
    /// An interface providing abstraction over a configuration source; that is, an object which is capable of
    /// loading a <see cref="IConfigSection"/> instance.
    /// </summary>
    public interface IConfigSource
    {
        /// <summary>
        /// Loads the <see cref="IConfiguration"/> instance from the origins represented by the current
        /// <see cref="IConfigSource"/> implementation.
        /// </summary>
        /// <returns>
        /// An <see cref="IConfiguration"/> instance representing the loaded configuration.
        /// </returns>
        IConfiguration LoadConfiguration();
    }
}