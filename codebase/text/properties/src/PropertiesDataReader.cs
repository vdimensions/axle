using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Kajabity.Tools.Java;

namespace Axle.Text.StructuredData.Properties
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class PropertiesDataReader : AbstractStructuredDataReader
    {
        private sealed class Adapter : AbstractStructuredDataAdapter
        {
            private readonly JavaProperties _propertiesFile;
            private readonly string _value;
            private readonly IDictionary<string, IStructuredDataAdapter[]> _children;

            public Adapter(JavaProperties propertiesFile, StringComparer comparer, string value = null)
            {
                _propertiesFile = propertiesFile;
                _value = value;
                var emptyProperties = new JavaProperties();
                _children = _propertiesFile.ToDictionary(
                    x => x.Key,
                    x => new IStructuredDataAdapter[] {new Adapter(emptyProperties, comparer, x.Value)},
                    comparer);
            }

            public override IDictionary<string, IStructuredDataAdapter[]> GetChildren() => _children;
            public override string Value => _value;
        }
        public PropertiesDataReader(StringComparer comparer) : base(comparer) { }

        protected override IStructuredDataAdapter CreateAdapter(Stream stream, Encoding encoding)
        {
            var propertiesFile = new JavaProperties();
            propertiesFile.Load(stream, encoding);
            return new Adapter(propertiesFile, Comparer);
        }

        protected override IStructuredDataAdapter CreateAdapter(string data)
        {
            var enc = Encoding.UTF8;
            return CreateAdapter(new MemoryStream(enc.GetBytes(data)), enc);
        }
    }
}
