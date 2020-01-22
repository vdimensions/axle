using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Kajabity.Tools.Java;

namespace Axle.Text.Data.Properties
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class PropertiesDataReader : AbstractTextDataReader
    {
        private sealed class Adapter : AbstractTextDataAdapter
        {
            public Adapter(JavaProperties propertiesFile) : this(string.Empty, null)
            {
                Children = propertiesFile.Select(x => new Adapter(x.Key, x.Value) as ITextDataAdapter);
            }
            private Adapter(string key, string value)
            {
                Key = key;
                Value = value;
                Children = Enumerable.Empty<ITextDataAdapter>();
            }

            public override string Key { get; }
            public override string Value { get; }
            public override IEnumerable<ITextDataAdapter> Children { get; }
        }
        public PropertiesDataReader(StringComparer comparer) : base(comparer) { }

        protected override ITextDataAdapter CreateAdapter(Stream stream, Encoding encoding)
        {
            var propertiesFile = new JavaProperties();
            propertiesFile.Load(stream, encoding);
            return new Adapter(propertiesFile);
        }

        protected override ITextDataAdapter CreateAdapter(string data)
        {
            var enc = Encoding.UTF8;
            return CreateAdapter(new MemoryStream(enc.GetBytes(data)), enc);
        }
    }
}
