using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.Properties.Extraction;

using NUnit.Framework;

namespace Axle.Resources.Properties.Tests
{
    using UriParser = Axle.Conversion.Parsing.UriParser;

    [TestFixture]
    public class PropertyResourceProviderTests
    {
        [Test]
        public void TestPropertyValueRetrieval()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                           .Configure("testBundle")
                               .Register(parser.Parse("Messages.properties"))
                               .Register(GetType().Assembly, "./Properties/");
            resourceManager.Extractors.Register(new PropertiesExtractor());

            var resource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsTrue(resource.HasValue, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resource.Value.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Value.Culture);

            using (var stream = resource.Value.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
        
        [Test]
        public void TestComplexPropertyValueRetrieval()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(parser.Parse("Messages.properties"))
                .Register(GetType().Assembly, "./Properties/");
            resourceManager.Extractors.Register(new PropertiesExtractor());

            var resource = resourceManager.Load("testBundle", "Alternative.Greeting", CultureInfo.CurrentCulture);

            Assert.IsTrue(resource.HasValue, "Unable to find Alternative.Greeting message");
            Assert.AreEqual("testBundle", resource.Value.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resource.Value.Culture);

            using (var stream = resource.Value.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
    }
}
