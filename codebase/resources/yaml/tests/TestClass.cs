using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.Extensions.IO.Stream;
using Axle.Resources.Extraction;
using Axle.Resources.Yaml.Extraction;

using NUnit.Framework;



namespace Axle.Resources.Yaml.Tests
{
    using UriParser = Axle.Conversion.Parsing.UriParser;

    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                           .Configure("testBundle")
                               .Register(parser.Parse("Messages.yml"))
                               .Register(parser.Parse("assembly://Axle.Resources.Yaml.Tests/Properties/"));
            resourceManager.Extractors.Register(new YamlExtractor());

            var resxResource = resourceManager.Resolve("testBundle", "Greeting", CultureInfo.CurrentCulture);

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
