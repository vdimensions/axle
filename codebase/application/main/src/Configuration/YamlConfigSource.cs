using System;
using Axle.Configuration.Text.Documents;
using Axle.Text.Documents;
using Axle.Text.Documents.Yaml;
using Axle.Verification;

namespace Axle.Configuration
{
    public sealed class YamlConfigSource : AbstractTextDocumentConfigSource
    {
        private readonly string _data;

        public YamlConfigSource(string data)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(data, nameof(data)));
            _data = data;
        }

        protected override ITextDocumentRoot ReadDocument()
        {
            var reader = new YamlDocumentReader(StringComparer.Ordinal);
            return reader.Read(_data);
        }
    }
}