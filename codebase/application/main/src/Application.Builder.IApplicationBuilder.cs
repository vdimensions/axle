using System;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Verification;

namespace Axle
{
    partial class Application
    {
        private sealed partial class Builder : IApplicationBuilder
        {
            IApplicationBuilder IApplicationBuilder.UseApplicationHost(IApplicationHost host)
            {
                Verifier.IsNotNull(Verifier.VerifyArgument(host, nameof(host)));
                _host = host;
                return this;
            }
        
            IApplicationBuilder IApplicationBuilder.ConfigureDependencies(Action<IDependencyContainer> setupContainerAction)
            {
                Verifier.IsNotNull(Verifier.VerifyArgument(setupContainerAction, nameof(setupContainerAction)));
                _onContainerReadyHandlers.Add(setupContainerAction);
                return this;
            }

            IApplicationBuilder IApplicationBuilder.ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfigurationAction)
            {
                setupConfigurationAction(this);
                return this;
            }

            IApplicationBuilder IApplicationBuilder.ConfigureModules(Action<IApplicationModuleConfigurer> setupModules)
            {
                setupModules.VerifyArgument(nameof(setupModules)).IsNotNull();
                setupModules(this);
                return this;
            }
        }
    }
}
