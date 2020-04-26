using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    public interface IApplicationHost
    {
        IDependencyContainerFactory DependencyContainerFactory { get; }
        ILoggingService LoggingService { get; }
        string EnvironmentName { get; }
        IConfiguration Configuration { get; }
        string[] Logo { get; }
    }
}