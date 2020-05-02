using System.IO;
using System.Reflection;
using Axle.Configuration;

namespace Axle
{
    partial class Application
    {
        private sealed partial class Builder : IConfigurationStreamProvider
        {
            Stream IConfigurationStreamProvider.LoadConfiguration(string file)
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                #else
                var appDir = Path.GetDirectoryName(typeof(Application).GetTypeInfo().Assembly.Location);
                #endif
                return File.OpenRead(Path.Combine(appDir, file));
            }
        }
    }
}
