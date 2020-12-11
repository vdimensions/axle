using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle.Application
{
    /// <summary>
    /// An interface representing an application host. An application host is responsible for supplying an application
    /// with environment-specific information and vital infrastructure objects, such as a 
    /// <see cref="IDependencyContainerFactory">dependency container factory</see>.
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
        /// Gets a <see cref="IConfiguration"/> object representing the default configuration values influenced 
        /// by the application host.
        /// </summary>
        IConfiguration HostConfiguration { get; }

        /// <summary>
        /// Gets a <see cref="IConfiguration"/> object representing the application settings defined in the
        /// configuration file specified by the <see cref="AppConfigFileName"/> with respect to the selected
        /// <see cref="EnvironmentName">environment name</see>.
        /// </summary>
        IConfiguration AppConfiguration { get; }

        /// <summary>
        /// Gets the application configuration filename (without the extension).
        /// </summary>
        string AppConfigFileName { get; }

        /// <summary>
        /// Gets the host configuration filename (without the extension).
        /// </summary>
        string HostConfigFileName { get; }
    }
}
