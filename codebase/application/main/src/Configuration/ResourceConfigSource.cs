#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Globalization;
using Axle.Resources;
using Microsoft.Extensions.Configuration;

namespace Axle.Configuration
{
    internal sealed class ResourceConfigSource<T> : IConfigSource where T : FileConfigurationSource, new()
    {
        private ResourceManager _resourceManager;
        private string _resourcePath;

        public IConfiguration LoadConfiguration()
        {
            var culture = CultureInfo.InvariantCulture;
            var resource = _resourceManager.Load(ConfigurationModule.BundleName, _resourcePath, culture);
            if (resource == null)
            {
                throw new ResourceNotFoundException(_resourcePath, ConfigurationModule.BundleName, culture);
            }
            var stream = resource.Open();
            return new StreamedFileConfigSource<T>(stream).LoadConfiguration();
        }
    }
}
#endif