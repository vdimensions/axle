using System;
using System.Collections.Generic;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Collections.Specialized;
#endif
using System.Linq;
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