using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.Extensions.IO.Stream;

using NUnit.Framework;


namespace Axle.Resources.Tests
{
    using UriParser = Axle.Conversion.Parsing.UriParser;

    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestFileResourceAccess()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                           .Configure("testBundle")
                                .Register(parser.Parse("file:///C:/Users/Ivaylo/Desktop/"))
                                .Register(parser.Parse("resx://Axle.Resources.Tests/Axle.Resources.Tests.ResXTestMessages"));

            var fileResource = resourceManager.Resolve("testBundle", "LL2H.txt", CultureInfo.CurrentCulture);

            Assert.IsNotNull(fileResource, "Unable to find LL2H.txt");
            Assert.AreEqual("testBundle", fileResource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, fileResource.Culture);

            using (var stream = fileResource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }
        }

        [Test]
        public void TestEmbeddedResourceAccess()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                .Configure("testBundle")
                .Register(parser.Parse("assembly://Axle.Resources.Tests/"));

            var fileResource = resourceManager.Resolve("testBundle", "EmbeddedText.txt", CultureInfo.CurrentCulture);

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

        [Test]
        public void TestResXResourceAccess()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles
                           .Configure("testBundle")
                           .Register(parser.Parse("resx://Axle.Resources.Tests/Axle.Resources.Tests.ResXTestMessages"));

            var resxResource = resourceManager.Resolve("testBundle", "Greeting", CultureInfo.CurrentCulture);

            Assert.IsNotNull(resxResource, "Unable to find Greeting message");
            Assert.AreEqual("testBundle", resxResource.Bundle);

            using (var stream = resxResource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Console.Write(data);
            }

            //Assert.AreEqual(CultureInfo.InvariantCulture, fileResource.Culture);
        }
    }
}
