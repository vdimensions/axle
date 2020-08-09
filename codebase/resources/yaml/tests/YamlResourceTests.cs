using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.Yaml.Extraction;

using NUnit.Framework;


namespace Axle.Resources.Yaml.Tests
{
    using UriParser = Axle.Conversion.Parsing.UriParser;

    [TestFixture]
    public class YamlResourceTests
    {
        [Test]
        public void TestValueRetrievalFromExplicitlyDefinedYamlFile()
        {
            var uriParser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(uriParser.Parse("invalid"))
                .Register(GetType().Assembly, "./Properties/");
            resourceManager.Extractors.Register(new YamlExtractor("Messages.yml"));

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
        
        [Test]
        public void TestValueRetrievalFromImplicitlyDiscoveredYamlFile()
        {
            var uriParser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(uriParser.Parse("Messages.yml"))
                .Register(GetType().Assembly, "./Properties/");
            resourceManager.Extractors.Register(new YamlExtractor());

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
        
        [Test]
        public void TestValueRetrievalFromImplicitlyDiscoveredYamlFileWithKeyPrefix()
        {
            var uriParser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(uriParser.Parse("Messages.yml/Prefixed/"))
                .Register(GetType().Assembly, "./Properties/");
            resourceManager.Extractors.Register(new YamlExtractor());

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
    }
}
