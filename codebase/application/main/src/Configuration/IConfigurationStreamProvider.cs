using System.IO;

namespace Axle.Configuration
{
    public interface IConfigurationStreamProvider
    {
        Stream LoadConfiguration(string configurationName);
    }
}