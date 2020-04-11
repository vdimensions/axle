using Axle.Configuration;

namespace Axle
{
    public interface IApplicationConfigurationBuilder
    {
        IApplicationConfigurationBuilder Prepend(IConfigSource configSource);
        IApplicationConfigurationBuilder Append(IConfigSource configSource);
    }
}