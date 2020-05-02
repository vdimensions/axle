using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    /// <summary>
    /// An interface representing an application host. An application host is responsible for supplying an application
    /// with environment-specific information and vital infrastructure objects, such as a 
    /// <see="IDependencyContainerFactory">dependency container factory</see>.

    /// </summary>
    public interface IApplicationHost
    {
        /// <summary>
        /// Gets the <see cref="IDependencyContainerFactory"/> implementation supplied by the current application host.
        /// </summary>
        IDependencyContainerFactory DependencyContainerFactory { get; }
        /// <summary>
        /// Gets the <see cref="ILoggingService"/> implementation provided by the current application host.
        /// </summary>
        ILoggingService LoggingService { get; }
        /// <summary>
        /// Gets a string representing the current environment name. The environment name is used to load a
        /// configuration layer on top of the default configuration that is used to provide environment-specific
        /// overrides. 
        /// </summary>
        string EnvironmentName { get; }
        /// <summary>
        /// Gets a <see cref="IConfiguration"/> object representing setting defaults influenced by the application host.
        /// </summary>
        IConfiguration Configuration { get; }
        /// <summary>
        /// Gets the application configuration filename (without the extension).
        /// </summary>
        string ApplicationConfigFileName { get; }
        /// <summary>
        /// Gets the application host configuration filename (without the extension).
        /// </summary>
        string HostConfigFileName { get; }
        /// <summary>
        /// Gets a <see cref="string">string</see> array of an ASCII logo to print to the console when the app starts.
        /// </summary>
        string[] AsciiLogo { get; }
    }
}
