using System;
using Axle.DependencyInjection;
using Axle.Verification;

namespace Axle.Application
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
        
            IApplicationBuilder IApplicationBuilder.ConfigureDependencies(Action<IDependencyContainer> setupDependencies)
            {
                Verifier.IsNotNull(Verifier.VerifyArgument(setupDependencies, nameof(setupDependencies)));
                _onContainerReadyHandlers.Add(setupDependencies);
                return this;
            }

            IApplicationBuilder IApplicationBuilder.ConfigureApplication(Action<IApplicationConfigurationBuilder> setupConfiguration)
            {
                setupConfiguration(this);
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
