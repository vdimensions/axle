using System.Globalization;
using System.IO;
using Axle.Configuration;
using Axle.Resources;

namespace Axle
{
    internal sealed class ResourceConfigurationStreamProvider : IConfigurationStreamProvider
    {
        private readonly ResourceManager _resourceManager;

        public ResourceConfigurationStreamProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        
        public Stream LoadConfiguration(string configFile)
        {
            return _resourceManager.Load(Application.ConfigBundleName, configFile, CultureInfo.InvariantCulture)?.Open();
        }
    }
}