#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Configuration.Adapters;
using Axle.Verification;

namespace Axle.Configuration
{
    using MSFileConfigurationSource = Microsoft.Extensions.Configuration.FileConfigurationSource;

    public sealed class FileConfigSource : IConfigSource
    {
        private readonly MSFileConfigurationSource _source;

        public FileConfigSource(MSFileConfigurationSource source)
        {
            _source = source.VerifyArgument(nameof(source)).IsNotNull();
        }

        public IConfiguration LoadConfiguration()
        {
            return MicrosoftConfigRoot.FromSource(_source);
        }
    }
}
#endif