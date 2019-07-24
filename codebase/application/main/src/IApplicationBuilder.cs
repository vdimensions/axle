using System;
using System.Collections.Generic;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    public interface IApplicationConfigurationBuilder
    {
        IApplicationConfigurationBuilder Prepend(IConfigSource configSource);
        IApplicationConfigurationBuilder Append(IConfigSource configSource);
    }
    public interface IApplicationBuilder
    {
        IApplicationBuilder SetDependencyContainerProvider(IDependencyContainerProvider containerProvider);
        IApplicationBuilder SetLoggingService(ILoggingServiceProvider loggingService);
        IApplicationBuilder ConfigureDependencies(Action<IContainer> setupContainerAction);

        IApplicationBuilder ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfigurationAction);

        IApplicationBuilder Load(IEnumerable<Type> types);
        IApplicationBuilder Load(Assembly assembly);
        IApplicationBuilder Load(IEnumerable<Assembly> assemblies);

        Application Run(params string[] args);
    }
}