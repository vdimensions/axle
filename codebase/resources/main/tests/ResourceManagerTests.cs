using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using Axle.Resources.Bundling;
using NUnit.Framework;


namespace Axle.Resources.Tests
{
    using UriParser = Axle.Conversion.Parsing.UriParser;

    [TestFixture]
    public class ResourceManagerTests
    {
        private static ResourceManager CreateResourceManager()
        {
            var parser = new UriParser();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles.Configure("testBundle")
                .RegisterApplicationRelativePath("./Content")
                .Register(parser.Parse("resx://Axle.Resources.Tests/Axle.Resources.Tests.ResXTestMessages"))
                .Register(parser.Parse("assembly://Axle.Resources.Tests/"));
            return resourceManager;
        }
        [Test]
        public void TestFileSystemResourceExtraction()
        {            
            var resourceManager = CreateResourceManager();
            var fileResource = resourceManager.Load("testBundle", "FileSystemTestFile.txt", CultureInfo.CurrentCulture);

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
        public void TestEmbeddedResourceExtraction()
        {
            var resourceManager = CreateResourceManager();

            var fileResource = resourceManager.Load("testBundle", "EmbeddedText.txt", CultureInfo.CurrentCulture);

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
        public void TestResXResourceExtraction()
        {
            var resourceManager = CreateResourceManager();

            var resxResource = resourceManager.Load("testBundle", "Greeting", CultureInfo.CurrentCulture);

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
