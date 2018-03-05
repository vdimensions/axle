using System.Reflection;


namespace Axle.Configuration
{
    public interface IConfigurationReader
    {
        IConfiguration LoadEmbeddedConfiguration(Assembly assembly, string resourceName);
        IConfiguration LoadEmbeddedConfiguration(Assembly assembly);

        IConfiguration CurrentConfiguration { get; }
    }
}