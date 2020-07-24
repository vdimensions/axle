using System.IO;

namespace Axle.Configuration
{
    internal interface IConfigurationStreamProvider
    {
        Stream LoadConfiguration(string configurationName);
    }
}