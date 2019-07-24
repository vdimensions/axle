using Axle.Configuration;
using Axle.Verification;


namespace Axle
{
    partial class Application : IApplicationConfigurationBuilder
    {
        IApplicationConfigurationBuilder IApplicationConfigurationBuilder.Prepend(IConfigSource configSource)
        {
            configSource.VerifyArgument(nameof(configSource)).IsNotNull();
            lock (_syncRoot)
            {
                ThrowIfStarted();
                _config = _config.Prepend(configSource);
            }
            return this;
        }

        IApplicationConfigurationBuilder IApplicationConfigurationBuilder.Append(IConfigSource configSource)
        {
            configSource.VerifyArgument(nameof(configSource)).IsNotNull();
            lock (_syncRoot)
            {
                ThrowIfStarted();
                _config = _config.Append(configSource);
            }
            return this;
        }
    }
}
