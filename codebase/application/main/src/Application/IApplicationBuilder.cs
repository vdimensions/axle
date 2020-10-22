using System;
using Axle.DependencyInjection;

namespace Axle.Application
{
    /// <summary>
    /// An interface for an application builder. The application builder is used to configure and start
    /// an axle <see cref="Axle.Application.Application">application</see>.
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// Enables the <see cref="Axle.Application.Application">application</see> to use a specific <see cref="IApplicationHost"/>
        /// instance.
        /// </summary>
        /// <param name="host">
        /// The application host to be used by the current <see cref="IApplicationBuilder">application builder</see>.
        /// </param>
        /// <returns>
        /// A reference to an <see cref="IApplicationBuilder">application builder</see> to proceed with the application
        /// configuration process, or to <see cref="Run">run</see> the application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="host"/> is <c>null</c>.
        /// </exception>
        IApplicationBuilder UseApplicationHost(IApplicationHost host);

        /// <summary>
        /// Allows for programmatically registering objects into the <see cref="Axle.Application.Application">application</see>'s root
        /// <see cref="IDependencyContainer">dependency container</see>.  
        /// </summary>
        /// <param name="setupDependencies">
        /// A delegate that is used to register dependencies to the <see cref="Axle.Application.Application">application</see>'s root
        /// <see cref="IDependencyContainer">dependency container</see>.  
        /// </param>
        /// <returns>
        /// A reference to an <see cref="IApplicationBuilder">application builder</see> to proceed with the application
        /// configuration process, or to <see cref="Run">run</see> the application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="setupDependencies"/> is <c>null</c>.
        /// </exception>
        IApplicationBuilder ConfigureDependencies(Action<IDependencyContainer> setupDependencies);

        /// <summary>
        /// Allows for programmatically registering additional configuration sources for configuring the
        /// <see cref="Axle.Application.Application">application</see>.  
        /// </summary>
        /// <param name="setupConfiguration">
        /// A delegate that is used to alter the configuration of the <see cref="Axle.Application.Application">application</see>.  
        /// </param>
        /// <returns>
        /// A reference to an <see cref="IApplicationBuilder">application builder</see> to proceed with the application
        /// configuration process, or to <see cref="Run">run</see> the application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="setupConfiguration"/> is <c>null</c>.
        /// </exception>
        IApplicationBuilder ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfiguration);

        /// <summary>
        /// Allows for programmatically enlisting application modules that should be ran by the 
        /// <see cref="Axle.Application.Application">application</see>.
        /// </summary>
        /// <param name="setupModules">
        /// A delegate that is used to register application modules with the current <see cref="IApplicationBuilder"/>.  
        /// </param>
        /// <returns>
        /// A reference to an <see cref="IApplicationBuilder">application builder</see> to proceed with the application
        /// configuration process, or to <see cref="Run">run</see> the application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="setupModules"/> is <c>null</c>.
        /// </exception>
        IApplicationBuilder ConfigureModules(Action<IApplicationModuleConfigurer> setupModules);

        /// <summary>
        /// Launches the <see cref="Axle.Application.Application">application</see> application based on the accumulated settings.
        /// </summary>
        /// <param name="args">
        /// A <see cref="string"/> array, containing the command-line arguments for running the application.
        /// </param>
        /// <returns>
        /// A reference to the running <see cref="Axle.Application.Application">application</see>.
        /// </returns>
        Application Run(params string[] args);
    }
}