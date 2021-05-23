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
        private readonly JavaProperties _properties;

        public PropertiesConfigSource(JavaProperties properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            _properties = properties;
        }
        public PropertiesConfigSource(Dictionary<string, string> properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            _properties = new JavaProperties(properties);
        }
        public PropertiesConfigSource(IDictionary<string, string> properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            _properties = new JavaProperties(new Dictionary<string, string>(properties));
        }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public PropertiesConfigSource(StringDictionary properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            _properties = new JavaProperties(
                properties.Keys.Cast<string>().Select(x => new KeyValuePair<string, string>(
                    x,
                    properties[x])).ToDictionary(x => x.Key, x => x.Value));
        }
        #endif
        public PropertiesConfigSource(string data)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(data, nameof(data)));
            var properties = new JavaProperties();
            var encoding = Encoding.UTF8;
            using (var memStream = new MemoryStream(encoding.GetBytes(data)))
            {
                properties.Load(memStream, encoding);
            }
            _properties = properties;
        }
        
        #if NETSTANDARD || NET45_OR_NEWER
        public PropertiesConfigSource(IReadOnlyDictionary<string, string> properties)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(properties, nameof(properties)));
            _properties = new JavaProperties(properties.ToDictionary(x => x.Key, x => x.Value));
        }
        #endif

        protected override ITextDocumentRoot ReadDocument()
        {
            var reader = new PropertiesDocumentReader(StringComparer.Ordinal);
            return reader.Read(reader.CreateAdapter(_properties));
        }
    }
}