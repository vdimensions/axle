using System.Globalization;
using System.Text;
using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using Axle.Resources.Bundling;
using Axle.Resources.Properties.Extraction;
using NUnit.Framework;

namespace Axle.Resources.Properties.Tests
{
    using UriParser = Axle.Text.Parsing.UriParser;

    [TestFixture]
    public class PropertyResourceProviderTests
    {
        [Test]
        public void TestValueRetrievalFromExplicitlyDefinedPropertiesFile()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(GetType().Assembly, "./Properties/")
                .Register(parser.Parse("invalid"))
                .Extractors.Register(new PropertiesExtractor("Messages.properties"));

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hello", data);
            }
        }
        
        [Test]
        public void TestValueRetrievalFromImplicitlyDiscoveredPropertiesFile()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(GetType().Assembly, "./Properties/")
                .Register(parser.Parse("Messages.properties"))
                .Extractors.Register(new PropertiesExtractor());

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hello", data);
            }
        }
        [Test]
        public void TestValueRetrievalFromImplicitlyDiscoveredPropertiesFileWithPathPrefix()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(GetType().Assembly, "./Properties/")
                .Register(parser.Parse("Messages.properties/Alternative/"))
                .Extractors.Register(new PropertiesExtractor());

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hi", data);
            }
        }
        
        [Test]
        public void TestValueRetrievalFromImplicitlyDiscoveredPropertiesFileWithFileNamePrefix()
        {
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(GetType().Assembly, "./Properties/")
                .Extractors.Register(new PropertiesExtractor("Messages.properties/Alternative/"));

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hi", data);
            }
        }
        
        [Test]
        public void TestValueRetrievalFromImplicitlyDiscoveredPropertiesFileWithExplicitPrefix()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(GetType().Assembly, "./Properties/")
                .Extractors.Register(new PropertiesExtractor("Messages.properties", "Alternative."));

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hi", data);
            }
        }
        
        [Test]
        public void TestComplexPropertyValueRetrieval()
        {
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle").Register(GetType().Assembly, "./Properties/")
                .Extractors.Register(new PropertiesExtractor("Messages.properties"));

            var resource = resourceManager.Load("testBundle", "Alternative.Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Alternative.Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hi", data);
            }
        }
        
        [Test]
        public void TestResouceLookupFromMultuplePropertySourcesUsingExplicitPrefix()
        {
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle").Register(GetType().Assembly, "./Properties/")
                .Extractors
                    .Register(new PropertiesExtractor("Messages.properties", "Alternative."))
                    .Register(new PropertiesExtractor("Strings.properties", "Alternative."));

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Alternative.Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hi", data);
            }
        }
        
        [Test]
        public void TestResouceLookupFromMultuplePropertySourcesUsingFileNamePrefix()
        {
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle").Register(GetType().Assembly, "./Properties/")
                .Extractors
                .Register(new PropertiesExtractor("Messages.properties/Alternative/"))
                .Register(new PropertiesExtractor("Strings.properties/Alternative/"));

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resource, "Unable to find Alternative.Greeting message");
            Assert.AreEqual("testBundle", resource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Culture);

            using (var stream = resource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Hi", data);
            }
        }
    }
}
