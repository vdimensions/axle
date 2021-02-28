using System;
using System.Globalization;
using System.Text;
using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using NUnit.Framework;

namespace Axle.Resources.Tests
{
    using UriParser = Axle.Text.Parsing.UriParser;

    [TestFixture]
    public class ResourceManagerEmbeddedResourcesTests
    {
        private static ResourceManager CreateResourceManager()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles.Configure("testBundle")
                .Register(parser.Parse("assembly://Axle.Resources.Tests/"));
            return resourceManager;
        }

        [Test]
        public void TestEmbeddedResourceExtraction()
        {
            var resourceManager = CreateResourceManager();

            var fileResource = resourceManager.Load("testBundle", "EmbeddedText.txt", CultureInfo.InvariantCulture);

            Assert.IsNotNull(fileResource, "Unable to find EmbeddedText.txt");
            Assert.AreEqual("testBundle", fileResource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, fileResource.Culture);

            using (var stream = fileResource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
    }
}
