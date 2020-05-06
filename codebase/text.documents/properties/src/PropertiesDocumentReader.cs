using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Kajabity.Tools.Java;

namespace Axle.Text.Documents.Properties
{
    /// <summary>
    /// A text document reader implementation that handles java properties files.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class PropertiesDocumentReader : AbstractTextDocumentReader
    {
        private sealed class Adapter : AbstractTextDocumentAdapter
        {
            public Adapter(JavaProperties propertiesFile) : this(string.Empty, null)
            {
                Children = propertiesFile.Select(x => new Adapter(x.Key, x.Value) as ITextDocumentAdapter);
            }
            private Adapter(string key, string value)
            {
                Key = key;
                Value = value;
                Children = Enumerable.Empty<ITextDocumentAdapter>();
            }

            public override string Key { get; }
            public override string Value { get; }
            public override IEnumerable<ITextDocumentAdapter> Children { get; }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="PropertiesDocumentReader"/> class.
        /// </summary>
        /// <param name="comparer">
        /// A <see cref="IEqualityComparer{T}"/> instance that will be used to compare the document node keys.
        /// </param>
        public PropertiesDocumentReader(IEqualityComparer<string> comparer) : base(comparer) { }

        protected override ITextDocumentAdapter CreateAdapter(Stream stream, Encoding encoding)
        {
            var propertiesFile = new JavaProperties();
            propertiesFile.Load(stream, encoding);
            return new Adapter(propertiesFile);
        }

        protected override ITextDocumentAdapter CreateAdapter(string data)
        {
            var enc = Encoding.UTF8;
            return CreateAdapter(new MemoryStream(enc.GetBytes(data)), enc);
        }
    }
}
