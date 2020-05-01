using Axle.Configuration;

namespace Axle
{
    public interface IApplicationConfigurationBuilder
    {
        IApplicationConfigurationBuilder Add(IConfigSource configSource);
    }
}