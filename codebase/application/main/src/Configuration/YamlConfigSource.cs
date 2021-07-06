using System;
using Axle.Configuration.Text.Documents;
using Axle.Text.Documents.Yaml;
using Axle.Verification;

namespace Axle.Configuration
{
    public sealed class YamlConfigSource : IConfigSource
    {
        private readonly IConfigSource _impl;

        public YamlConfigSource(string data)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(data, nameof(data)));
            _impl = new TextDocumentConfigSource(new YamlDocumentReader(StringComparer.Ordinal), data);
        }

        public IConfiguration LoadConfiguration() => _impl.LoadConfiguration();
    }
}