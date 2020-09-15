using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Axle.Text.Documents;

namespace Axle.Resources.Binding
{
    public sealed class ResourceDocumentRoot : ITextDocumentRoot
    {
        private sealed class ResourceDocumentNode : ITextDocumentObject
        {
            private readonly ResourceManager _resourceManager;
            private readonly string _bundle;
            private readonly string _prefix;

            public ResourceDocumentNode(ResourceManager resourceManager, string bundle, string prefix, string key, ITextDocumentObject parent)
            {
                _resourceManager = resourceManager;
                _bundle = bundle;
                _prefix = prefix;
                Key = key;
                Parent = parent;
            }
            
            public IEnumerable<ITextDocumentNode> GetChildren() => Enumerable.Empty<ITextDocumentNode>();

            public IEnumerable<ITextDocumentNode> GetValues(string name)
            {
                var prefixOrKey = _prefix.Length == 0 ? name : $"{_prefix}.{name}";
                var resource = _resourceManager.Load<string>(_bundle, prefixOrKey, CultureInfo.CurrentUICulture);
                if (!string.IsNullOrEmpty(resource))
                {
                    yield return new TextDocumentValue(name, this, resource);
                }
                else
                {
                    yield return new ResourceDocumentNode(_resourceManager, _bundle, prefixOrKey, name, this);
                }
            }

            public ITextDocumentObject Parent { get; }
            public string Key { get; }
            public IEqualityComparer<string> KeyComparer => StringComparer.Ordinal;
        }
        
        private readonly ResourceDocumentNode _impl;

        public ResourceDocumentRoot(ResourceManager resourceManager, string bundle)
        {
            _impl = new ResourceDocumentNode(resourceManager, bundle, string.Empty, string.Empty, null);
        }

        public IEnumerable<ITextDocumentNode> GetChildren() => _impl.GetChildren();

        public IEnumerable<ITextDocumentNode> GetValues(string name) => _impl.GetValues(name);

        public ITextDocumentObject Parent => _impl.Parent;

        public string Key => _impl.Key;

        public IEqualityComparer<string> KeyComparer => _impl.KeyComparer;
    }
}