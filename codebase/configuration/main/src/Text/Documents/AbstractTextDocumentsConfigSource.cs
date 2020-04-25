using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Text.Documents;

namespace Axle.Configuration.Text.Documents
{
    /// <summary>
    /// A class to serve as a base for implementing the <see cref="IConfigSource"/> interface that is based on a
    /// <see cref="ITextDocumentRoot"/>.
    /// </summary>
    /// <seealso cref="IConfigSource"/>
    /// <seealso cref="ITextDocumentRoot"/>
    public abstract class AbstractTextDocumentsConfigSource : IConfigSource
    {
        private abstract class AbstractTextDocumentConfig<TNode> where TNode: ITextDocumentNode
        {
            protected AbstractTextDocumentConfig(TNode documentNode)
            {
                DocumentNode = documentNode;
            }

            protected TNode DocumentNode { get; }

            public string Name => DocumentNode.Key;
            
            public IEnumerable<string> Keys
            {
                get
                {
                    return DocumentNode is ITextDocumentObject o
                        ? Enumerable.Select(o.GetChildren(), x => x.Key)
                        : Enumerable.Empty<string>();
                }
            }
        }

        private sealed class TextDocumentConfigSetting : AbstractTextDocumentConfig<ITextDocumentValue>, IConfigSetting
        {
            public TextDocumentConfigSetting(ITextDocumentValue documentNode) : base(documentNode) { }
            
            public string Value => DocumentNode.Value;
        }
        private sealed class TextDocumentConfigSection : AbstractTextDocumentConfig<ITextDocumentObject>, IConfigSection
        {
            public TextDocumentConfigSection(ITextDocumentObject documentNode) : base(documentNode) { }

            public IConfigSetting this[string key]
            {
                get
                {
                    var node = DocumentNode.GetChildren().SingleOrDefault(c => DocumentNode.KeyComparer.Equals(key, c.Key));
                    switch (node)
                    {
                        case null:
                            return null;
                        case ITextDocumentObject o:
                            return new TextDocumentConfigSection(o);
                        case ITextDocumentValue v:
                            return new TextDocumentConfigSetting(v);
                        default:
                            throw new InvalidOperationException($"Unknown document node type {node.GetType()}");
                    }
                }
            }

            public string Value => null;
        }
        private sealed class TextDocumentConfiguration : IConfiguration
        {
            private readonly TextDocumentConfigSection _configSection;
            
            public TextDocumentConfiguration(ITextDocumentRoot documentNode)
            {
                _configSection = new TextDocumentConfigSection(documentNode);
            }

            public string Value => _configSection.Value;
            public IEnumerable<string> Keys => _configSection.Keys;
            public string Name => _configSection.Name;

            public IConfigSetting this[string key] => _configSection[key];
        }

        /// <inheritdoc />
        public IConfiguration LoadConfiguration() => new TextDocumentConfiguration(ReadDocument());
        
        protected abstract ITextDocumentRoot ReadDocument();
        //ITextDocumentReader
    }
}