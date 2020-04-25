using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Axle.IO.Extensions.Stream;
using Axle.Verification;
#if NETSTANDARD && !NETSTANDARD1_3_OR_NEWER
using Axle.Text.Extensions.Encoding;
#endif

namespace Axle.Text.Documents
{
    /// <summary>
    /// An abstract class aiding in the implementation of the <see cref="ITextDocumentReader"/> interface.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract class AbstractTextDocumentReader : ITextDocumentReader
    {
        private static IEnumerable<ITextDocumentNode> FixHierarchy(string key, IEnumerable<ITextDocumentNode> nodes)
        {
            var tokenizedKey = TextDocumentObject.Tokenize(key);
            var currentNodes = nodes;
            if (tokenizedKey.Length > 1)
            {
                var fixedKey = tokenizedKey[tokenizedKey.Length - 1];
                currentNodes = currentNodes.Select(
                    node =>
                    {
                        switch (node)
                        {
                            case ITextDocumentValue v:
                                return new TextDocumentValue(fixedKey, null, v.Value);
                            case ITextDocumentObject o:
                                return new TextDocumentObject(fixedKey, null, o.GetChildren(), false);
                            default:
                                return node;
                        }
                    });
            }
            for (var i = tokenizedKey.Length - 2; i >= 0; i--)
            {
                currentNodes = new[] {new TextDocumentObject(tokenizedKey[i], null, currentNodes, false)};
            }
            return currentNodes;
        }
        
        private readonly StringComparer _comparer;

        protected AbstractTextDocumentReader(StringComparer comparer)
        {
            _comparer = comparer;
        }

        protected virtual ITextDocumentAdapter CreateAdapter(Stream stream, Encoding encoding)
        {
            return CreateAdapter(encoding.GetString(stream.ToByteArray()));
        }
        protected abstract ITextDocumentAdapter CreateAdapter(string data);
        
        private ITextDocumentRoot ReadStructuredData(
            ITextDocumentAdapter adapter, 
            StringComparer comparer)
        {
            return new TextDocumentRoot(ReadStructuredData(string.Empty, adapter), comparer);
        }

        private IEnumerable<ITextDocumentNode> ExpandChildren(ITextDocumentAdapter adapter)
        {
            foreach (var childGroup in adapter.Children.GroupBy(x => x.Key))
            foreach (var child in childGroup)
            foreach (var node in FixHierarchy(childGroup.Key, ReadStructuredData(childGroup.Key, child)))
            {
                yield return node;
            }
        }
        
        private IEnumerable<ITextDocumentNode> ReadStructuredData(string key, ITextDocumentAdapter adapter)
        {
            var isRoot = key.Length == 0;
            if (isRoot)
            {
                foreach (var node in ExpandChildren(adapter))
                {
                    yield return node;
                }
            }
            else
            {
                if (adapter.Value != null)
                {
                    yield return new TextDocumentValue(key, null, adapter.Value);
                }
                var children = ExpandChildren(adapter);
                if (children.Any())
                {
                    yield return new TextDocumentObject(key, null, children, false);
                }
            }
        }

        public ITextDocumentRoot Read(Stream stream, Encoding encoding)
        {
            stream.VerifyArgument(nameof(stream)).IsNotNull();
            encoding.VerifyArgument(nameof(encoding)).IsNotNull();
            return ReadStructuredData(CreateAdapter(stream, encoding), _comparer);
        }

        public ITextDocumentRoot Read(string data)
        {
            data.VerifyArgument(nameof(data)).IsNotNull();
            return ReadStructuredData(CreateAdapter(data), _comparer);
        }

        protected StringComparer Comparer => _comparer;
    }
}