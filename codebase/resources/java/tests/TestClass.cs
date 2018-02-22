﻿using System;
using System.Globalization;
using System.Text;

using Axle.Conversion;
using Axle.Extensions.IO.Stream;
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
                               .Register(parser.Parse("TestMessages.properties"))
                               .Register(parser.Parse("file:///C:/Users/Ivaylo/Desktop/"));
            resourceManager.Extractors
                           .Register(new JavaPropertiesFileExtractor())
                           .Register(new JavaPropertiesValueExtractor());

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