using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using Axle.Resources.Extraction;
using Axle.Resources.Java.Extraction;

using NUnit.Framework;



namespace Axle.Resources.Java.Tests
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
                               .Register(parser.Parse("Messages.properties"))
                               .Register(parser.Parse("assembly://Axle.Resources.Java.Tests/Properties/"));
            resourceManager.Extractors.Register(new JavaPropertiesExtractor());

            var resxResource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsTrue(resxResource.HasValue, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resxResource.Value.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, resxResource.Value.Culture);

            using (var stream = resxResource.Value.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }
    }
}
