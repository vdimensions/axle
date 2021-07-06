#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.IO;
using Axle.IO.Extensions.Stream;
using Microsoft.Extensions.FileProviders;

namespace Axle.Configuration.Microsoft
{
    using MSFileConfigurationSource = global::Microsoft.Extensions.Configuration.FileConfigurationSource;

    public sealed class StreamedFileConfigSource<T> : IConfigSource
        where T: MSFileConfigurationSource, new()
    {
        private readonly FileConfigSource _fileConfigSource;

        public StreamedFileConfigSource(Stream stream)
        {
            var tempFile = Path.GetTempFileName();
            using (stream)
            using (var fileStr = File.OpenWrite(tempFile))
            {
                stream.Seek(0, SeekOrigin.Begin);
                StreamExtensions.WriteTo(stream, fileStr, 8192);
                fileStr.Flush();
            }

            _fileConfigSource = new FileConfigSource(
                new T
                {
                    Path = Path.GetFileName(tempFile), 
                    FileProvider = new PhysicalFileProvider(Path.GetDirectoryName(tempFile))
                });
        }

        public IConfiguration LoadConfiguration()
        {
            return _fileConfigSource.LoadConfiguration();
        }
    }
}
#endif