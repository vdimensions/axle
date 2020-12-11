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
                lock (_syncRoot)
                {
                    _host = host;
                }
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
                Verifier.IsNotNull(Verifier.VerifyArgument(setupConfiguration, nameof(setupConfiguration)));
                lock (_syncRoot)
                {
                    setupConfiguration(this);
                }
                return this;
            }

            IApplicationBuilder IApplicationBuilder.ConfigureModules(Action<IApplicationModuleConfigurer> setupModules)
            {
                Verifier.IsNotNull(Verifier.VerifyArgument(setupModules, nameof(setupModules)));
                lock (_syncRoot)
                {
                    setupModules(this);
                }
                return this;
            }
        }
    }
}
