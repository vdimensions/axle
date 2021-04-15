using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Axle.Text.Documents;

namespace Axle.Resources.Binding
{
    /// <summary>
    /// An implementation of the <see cref="ITextDocumentRoot"/> interface that is backed by
    /// a <see cref="ResourceManager"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class ResourceDocumentRoot : ITextDocumentRoot
    {
        private sealed class ResourceDocumentNode : ITextDocumentObject
        {
            private readonly ResourceManager _resourceManager;
            private readonly string _bundle;
            private readonly string _prefix;

            public ResourceDocumentNode(
                ResourceManager resourceManager, 
                string bundle, 
                string prefix, 
                string key, 
                ITextDocumentObject parent)
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

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceDocumentRoot"/> class using the provided 
        /// <paramref name="resourceManager">resource manager</paramref> instance and resource <paramref name="bundle"/>
        /// name.
        /// </summary>
        /// <param name="resourceManager">
        /// The resource manager that will be responsible for providing values for the document keys.
        /// </param>
        /// <param name="bundle">
        /// The resource bundle associated with the document contents.
        /// </param>
        public ResourceDocumentRoot(ResourceManager resourceManager, string bundle)
        {
            _impl = new ResourceDocumentNode(resourceManager, bundle, string.Empty, string.Empty, null);
        }

        /// <inheritdoc />
        public IEnumerable<ITextDocumentNode> GetChildren() => _impl.GetChildren();

        /// <inheritdoc />
        public IEnumerable<ITextDocumentNode> GetValues(string name) => _impl.GetValues(name);

        /// <inheritdoc />
        public ITextDocumentObject Parent => _impl.Parent;

        /// <inheritdoc />
        public string Key => _impl.Key;

        /// <inheritdoc />
        public IEqualityComparer<string> KeyComparer => _impl.KeyComparer;
    }
}