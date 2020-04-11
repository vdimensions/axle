using System;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder SetDependencyContainerProvider(IDependencyContainerFactory containerFactory);
        
        IApplicationBuilder SetLoggingService(ILoggingService loggingService);

        IApplicationBuilder ConfigureDependencies(Action<IDependencyContainer> setupContainerAction);

        IApplicationBuilder ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfigurationAction);

        IApplicationBuilder ConfigureModules(Action<IApplicationModuleConfigurer> setupModules);

        Application Run(params string[] args);
    }
}