using System;
using System.Collections.Generic;
using System.IO;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Collections.Specialized;
#endif
using System.Linq;
using System.Text;
using Axle.Configuration.Text.Documents;
using Axle.Text.Documents;
using Axle.Text.Documents.Properties;
using Axle.Verification;
using Kajabity.Tools.Java;

namespace Axle.Configuration
{
    public sealed class PropertiesConfigSource : AbstractTextDocumentConfigSource
    {
        private readonly ITextDocumentRoot _document;

        public PropertiesConfigSource(JavaProperties properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            _document = reader.Read(reader.CreateAdapter(properties));
        }
        public PropertiesConfigSource(Dictionary<string, string> properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            _document = reader.Read(reader.CreateAdapter(new JavaProperties(properties)));
        }
        public PropertiesConfigSource(IDictionary<string, string> properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            _document = reader.Read(reader.CreateAdapter(new JavaProperties(new Dictionary<string, string>(properties))));
        }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public PropertiesConfigSource(StringDictionary properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            _document = reader.Read(
                reader.CreateAdapter(new JavaProperties(
                    properties.Keys.Cast<string>()
                        .Select(x => new KeyValuePair<string, string>(x, properties[x]))
                        .ToDictionary(x => x.Key, x => x.Value))));
        }
        #endif
        public PropertiesConfigSource(string data)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(data, nameof(data)));
            var properties = new JavaProperties();
            var encoding = Encoding.UTF8;
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            using (var memStream = new MemoryStream(encoding.GetBytes(data)))
            {
                properties.Load(memStream, encoding);
            }
            _document = reader.Read(reader.CreateAdapter(properties));
        }
        
        #if NETSTANDARD || NET45_OR_NEWER
        public PropertiesConfigSource(IReadOnlyDictionary<string, string> properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            _document = reader.Read(reader.CreateAdapter(new JavaProperties(properties.ToDictionary(x => x.Key, x => x.Value))));
        }
        #endif

        protected override ITextDocumentRoot ReadDocument() => _document;
    }
}