using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace Axle.Text.Documents.Xml.Tests
{
    [TestFixture]
    public class XmlReaderTests
    {
        [Test]
        public void TestXDocumentDataLookup()
        {
            var propertiesPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "data.xml");
            var reader = new XDocumentReader(StringComparer.OrdinalIgnoreCase);
            TestData(reader, propertiesPath);
        }
        
        [Test]
        public void TestXmlDocumentDataLookup()
        {
            var propertiesPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "data.xml");
            var reader = new XmlTextDocumentReader(StringComparer.OrdinalIgnoreCase);
            TestData(reader, propertiesPath);
        }

        private static void TestData(ITextDocumentReader reader, string propertiesPath)
        {
            var data = reader.Read(File.OpenRead(propertiesPath), Encoding.UTF8);

            var item1 = data.GetValues("SingleKey").SingleOrDefault() as ITextDocumentValue;
            var item2 = data.GetValues("System");
            var item3 = data.GetValues("System.Text").OfType<ITextDocumentObject>();
            var item4 = data.GetValues("System.Text.Encoding").FirstOrDefault() as ITextDocumentValue;
            var item5 = data.GetValues("System.Text.DefaultEncoding").FirstOrDefault() as ITextDocumentValue;
            var item6 = item3.FirstOrDefault()?.GetValues("Encoding").FirstOrDefault() as ITextDocumentValue;
            var item7 = item3.FirstOrDefault()?.GetValues("DefaultEncoding").FirstOrDefault() as ITextDocumentValue;

            Assert.IsNotNull(item1, "Lookup for value failed for simple key {0}", "SingleKey");
            Assert.IsNotNull(item2, "Lookup for object failed for simple key {0}", "System");
            Assert.IsNotNull(item3, "Lookup for object failed for complex key {0}", "System.Text");
            Assert.IsNotNull(item4, "Lookup for value failed for complex key {0}", "System.Text.Encoding");
            Assert.IsNotNull(item5, "Lookup for value failed for complex key {0}", "System.Text.DefaultEncoding");
            Assert.IsNotNull(item6, "Lookup for value in object {1} failed for simple key {0}", "Encoding", "System.Text");
            Assert.IsNotNull(item7, "Lookup for value in object {1} failed for simple key {0}", "DefaultEncoding", "System.Text");
        }
    }
}
