using System.IO;
using System.Reflection;
using Axle.IO.Extensions.Stream;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NUnit.Framework;

namespace Axle.Configuration.Json.Tests
{
    public class ModularityTests
    {
        internal sealed class StreamFileProvider
        {
            public static string GetFile(Stream stream)
            {
                var fileName = Path.GetFileName(Path.GetTempFileName());
                var tmpFileName =
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                        fileName
                    );
                using (var w = File.OpenWrite(tmpFileName))
                {
                    stream.WriteTo(w, 4096);
                    stream.Flush();
                }
                return fileName;
            }

            public static T GetFileConfigSource<T>(Stream stream) where T : FileConfigurationSource, new()
            {
                return new T { Path = GetFile(stream) };
            }

            public static IConfigurationRoot GetConfiguration<T>(Stream stream) where T : FileConfigurationSource, new()
            {
                var source = GetFileConfigSource<T>(stream);
                var b = new ConfigurationBuilder();
                b.Sources.Add(source);
                return b.Build();
            }
        }

        [Test]
        public void Test()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("jsconfig.json");
            var cfg = builder.Build();
            var message = cfg["message"];
            Assert.IsNotNull(message);
        }

        [Test]
        public void TestStreamedJsonConfig()
        {
            var cfgPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\jsconfig.json";
            var cfg = StreamFileProvider.GetConfiguration<JsonConfigurationSource>(File.OpenRead(cfgPath));
            var message = cfg["message"];
            Assert.IsNotNull(message);
        }
    }
}