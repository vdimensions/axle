using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Verification;


namespace Axle
{
    partial class Application : IApplicationBuilder
    {
        IApplicationBuilder IApplicationBuilder.SetDependencyContainerProvider(IDependencyContainerProvider containerProvider)
        {
            lock (_syncRoot)
            {
                ThrowIfStarted();
                _dependencyContainerProvider = containerProvider;
            }
            return this;
        }

        IApplicationBuilder IApplicationBuilder.SetLoggingService(ILoggingServiceProvider loggingService)
        {
            lock (_syncRoot)
            {
                ThrowIfStarted();
                _loggingService = loggingService;
            }
            return this;
        }

        IApplicationBuilder IApplicationBuilder.ConfigureDependencies(Action<IContainer> setupContainerAction)
        {
            ThrowIfStarted();
            Verifier.IsNotNull(Verifier.VerifyArgument(setupContainerAction, nameof(setupContainerAction)));
            _onContainerReadyHandlers.Add(setupContainerAction);
            return this;
        }

        IApplicationBuilder IApplicationBuilder.ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfigurationAction)
        {
            setupConfigurationAction(this);
            return this;
        }

        IApplicationBuilder IApplicationBuilder.Load(IEnumerable<Type> types) => Load(types);
        IApplicationBuilder IApplicationBuilder.Load(Assembly assembly)
        {
            assembly.VerifyArgument(nameof(assembly)).IsNotNull();
            return Load(_moduleCatalog.DiscoverModuleTypes(assembly));
        }
        IApplicationBuilder IApplicationBuilder.Load(IEnumerable<Assembly> assemblies)
        {
            return Load(assemblies.SelectMany(a => _moduleCatalog.DiscoverModuleTypes(a)));
        }
    }
}
