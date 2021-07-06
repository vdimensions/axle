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
    public class ResourceManagerResXTests
    {
        private static ResourceManager CreateResourceManager()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles.Configure("testBundle")
                .Register(parser.Parse("resx://Axle.Resources.Tests/Axle.Resources.Tests.ResXTestMessages"));
            return resourceManager;
        }
        
        [Test]
        public void TestResXResourceExtraction()
        {
            var resourceManager = CreateResourceManager();

            var resxResource = resourceManager.Load("testBundle", "Greeting", CultureInfo.InvariantCulture);

            Assert.IsNotNull(resxResource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resxResource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resxResource.Culture);

            using (var stream = resxResource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
    }
}
