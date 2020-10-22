using Axle.Configuration;
using Axle.Verification;


namespace Axle.Application
{
    partial class Application
    {
        sealed partial class Builder : IApplicationConfigurationBuilder
        {
            IApplicationConfigurationBuilder IApplicationConfigurationBuilder.Add(IConfigSource configSource)
            {
                configSource.VerifyArgument(nameof(configSource)).IsNotNull();
                lock (_syncRoot)
                {
                    _config = _config.Append(configSource);
                }
                return this;
            }
        }
    }
}
