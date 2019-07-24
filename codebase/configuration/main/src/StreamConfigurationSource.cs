#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.IO;
using System.Reflection;
using Axle.IO.Extensions.Stream;

namespace Axle.Configuration
{
    using MSFileConfigurationSource = Microsoft.Extensions.Configuration.FileConfigurationSource;

    public sealed class StreamedFileConfigSource<T> : IConfigSource
        where T: MSFileConfigurationSource, new()
    {
        private readonly Stream _stream;

        public StreamedFileConfigSource(Stream stream)
        {
            _stream = stream;
        }

        public IConfiguration LoadConfiguration()
        {
            var randomFileName = Path.GetFileName(Path.GetTempFileName());
            var tmpFileName =
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                    randomFileName
                );
            using (_stream)
            using (_stream.DumpTo(tmpFileName))
            {
                return new FileConfigSource(new T { Path = randomFileName }).LoadConfiguration();
            }
        }
    }
}
#endif