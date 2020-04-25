using System;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    public interface IApplicationBuilder
    {
        [Obsolete]
        IApplicationBuilder SetDependencyContainerProvider(IDependencyContainerFactory containerFactory);
        
        [Obsolete]
        IApplicationBuilder SetLoggingService(ILoggingService loggingService);

        IApplicationBuilder SetApplicationHost(IApplicationHost host);

        IApplicationBuilder ConfigureDependencies(Action<IDependencyContainer> setupContainerAction);

        IApplicationBuilder ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfigurationAction);

        IApplicationBuilder ConfigureModules(Action<IApplicationModuleConfigurer> setupModules);

        Application Run(params string[] args);
    }
}