using System;
using System.Globalization;
using System.Text;
using Axle.Conversion;
using Axle.IO.Extensions.Stream;
using Axle.Resources.Bundling;
using NUnit.Framework;

namespace Axle.Resources.Tests
{
    [TestFixture]
    public class ResourceManagerFileSystemResourcesTests
    {
        private static ResourceManager CreateResourceManager()
        {
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles.Configure("testBundle")
                .RegisterApplicationRelativePath("./Content");
            return resourceManager;
        }
        
        [Test]
        public void TestFileSystemResourceExtraction()
        {            
            var resourceManager = CreateResourceManager();
            var fileResource = resourceManager.Load("testBundle", "FileSystemTestFile.txt", CultureInfo.InvariantCulture);

            Assert.IsNotNull(fileResource, "Unable to find FileSystemTestFile.txt");
            Assert.AreEqual("testBundle", fileResource.Bundle);
            Assert.AreEqual(CultureInfo.InvariantCulture, fileResource.Culture);

            using (var stream = fileResource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Text", data);
            }
        }
        
        [Test]
        public void TestFileSystemResourceExtractionWithCulture()
        {            
            var resourceManager = CreateResourceManager();
            var culture = CultureInfo.CreateSpecificCulture("en");
            if (culture.Parent.Name.Equals("en"))
            {
                // .NET may try to use a concrete culture here, we make sure we are using "en" exactly
                culture = culture.Parent;
            }
            var fileResource = resourceManager.Load("testBundle", "FileSystemTestFile.txt", culture);

            Assert.IsNotNull(fileResource, "Unable to find FileSystemTestFile.txt");
            Assert.AreEqual("testBundle", fileResource.Bundle);
            Assert.AreEqual(culture, fileResource.Culture);

            using (var stream = fileResource.Open())
            {
                var data = new BytesToStringConverter(Encoding.UTF8).Convert(stream.ToByteArray());
                Assert.IsNotNull(data);
                Assert.AreEqual("Text.en", data);
            }
        }
    }
}
