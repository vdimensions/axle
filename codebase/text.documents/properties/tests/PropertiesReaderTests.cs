using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Axle.Text.Documents.Binding;
using Axle.Text.Parsing;
using NUnit.Framework;

namespace Axle.Text.Documents.Properties.Tests
{
    [TestFixture]
    public class PropertiesReaderTests
    {
        private sealed class HasDictionaryProperty
        {
            public enum Key
            {
                Text
            }
            public Dictionary<Key, Dictionary<string, string>> System { get; set; } = new Dictionary<Key, Dictionary<string, string>>();
        }
        
        [Test]
        public void TestDataLookup()
        {
            var propertiesPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "data.properties");
            var reader = new PropertiesDocumentReader(StringComparer.OrdinalIgnoreCase);
            var data = reader.Read(File.OpenRead(propertiesPath), Encoding.UTF8);

            var item1 = data.GetValues("SingleKey").SingleOrDefault() as ITextDocumentValue;
            var item2 = data.GetValues("System");
            var item3 = data.GetValues("System.Text");
            var item4 = data.GetValues("System.Text.Encoding").SingleOrDefault() as ITextDocumentValue;
            var item5 = data.GetValues("System.Text.DefaultEncoding").SingleOrDefault() as ITextDocumentValue;
            var item6 = item3.OfType<ITextDocumentObject>().Take(1).SingleOrDefault()?.GetValues("Encoding").SingleOrDefault() as ITextDocumentValue;
            var item7 = item3.OfType<ITextDocumentObject>().Skip(1).Take(1).SingleOrDefault()?.GetValues("DefaultEncoding").SingleOrDefault() as ITextDocumentValue;
            
            Assert.IsNotNull(item1, "Lookup for value failed for simple key {0}", "SingleKey");
            Assert.IsNotNull(item2, "Lookup for object failed for simple key {0}", "System");
            Assert.IsNotNull(item3, "Lookup for object failed for complex key {0}", "System.Text");
            Assert.IsNotNull(item4, "Lookup for value failed for complex key {0}", "System.Text.Encoding");
            Assert.IsNotNull(item5, "Lookup for value failed for complex key {0}", "System.Text.DefaultEncoding");
            Assert.IsNotNull(item6, "Lookup for value in object {1} failed for simple key {0}", "Encoding", "System.Text");
            Assert.IsNotNull(item7, "Lookup for value in object {1} failed for simple key {0}", "DefaultEncoding", "System.Text");
        }
        
        [Test]
        public void TestDictionaryBinding()
        {
            var propertiesPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "data.properties");
            var reader = new PropertiesDocumentReader(StringComparer.OrdinalIgnoreCase);
            var data = reader.Read(File.OpenRead(propertiesPath), Encoding.UTF8);

            var dictionary = new Dictionary<string, string>();
            var binder = new DefaultDocumentBinder();
            binder.Bind(data, dictionary);
            
            Assert.IsNotNull(dictionary);
            Assert.IsNotEmpty(dictionary);
        }
        
        [Test]
        public void TestNestedDictionaryBinding()
        {
            var propertiesPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "data.properties");
            var reader = new PropertiesDocumentReader(StringComparer.OrdinalIgnoreCase);
            var data = reader.Read(File.OpenRead(propertiesPath), Encoding.UTF8);

            var converter = new BindingConverter();
            var binder = new DefaultDocumentBinder(new ReflectionObjectProvider(), converter);
            converter.RegisterConverter<HasDictionaryProperty.Key>(new EnumParser<HasDictionaryProperty.Key>());

            var result = new HasDictionaryProperty();
            binder.Bind(data, result);
            Assert.IsNotNull(result.System);
            Assert.IsNotEmpty(result.System);
            Assert.IsNotEmpty(result.System[HasDictionaryProperty.Key.Text]);
        }
    }
}
