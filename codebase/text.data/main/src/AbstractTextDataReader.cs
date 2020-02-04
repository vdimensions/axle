using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Axle.Text.Data
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract class AbstractTextDataReader : ITextDataReader
    {
        private static IEnumerable<ITextDataNode> FixHierarchy(string key, IEnumerable<ITextDataNode> nodes)
        {
            var tokenizedKey = TextDataObject.Tokenize(key);
            var currentNodes = nodes;
            if (tokenizedKey.Length > 1)
            {
                var fixedKey = tokenizedKey[tokenizedKey.Length - 1];
                currentNodes = currentNodes.Select(
                    node =>
                    {
                        switch (node)
                        {
                            case ITextDataValue v:
                                return new TextDataValue(fixedKey, null, v.Value);
                            case ITextDataObject o:
                                return new TextDataObject(fixedKey, null, o.GetChildren(), false);
                            default:
                                return node;
                        }
                    });
            }
            for (var i = tokenizedKey.Length - 2; i >= 0; i--)
            {
                currentNodes = new[] {new TextDataObject(tokenizedKey[i], null, currentNodes, false)};
            }
            return currentNodes;
        }
        
        private readonly StringComparer _comparer;

        protected AbstractTextDataReader(StringComparer comparer)
        {
            _comparer = comparer;
        }

        protected abstract ITextDataAdapter CreateAdapter(Stream stream, Encoding encoding);
        protected abstract ITextDataAdapter CreateAdapter(string data);
        
        private ITextDataRoot ReadStructuredData(
            ITextDataAdapter adapter, 
            StringComparer comparer)
        {
            return new TextDataObjectRoot(ReadStructuredData(string.Empty, adapter), comparer);
        }

        private IEnumerable<ITextDataNode> ExpandChildren(ITextDataAdapter adapter)
        {
            foreach (var childGroup in adapter.Children.GroupBy(x => x.Key))
            foreach (var child in childGroup)
            foreach (var node in FixHierarchy(childGroup.Key, ReadStructuredData(childGroup.Key, child)))
            {
                yield return node;
            }
        }
        
        private IEnumerable<ITextDataNode> ReadStructuredData(
            string key,
            ITextDataAdapter adapter)
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
                    yield return new TextDataValue(key, null, adapter.Value);
                }
                var children = ExpandChildren(adapter);
                if (children.Any())
                {
                    yield return new TextDataObject(key, null, children, false);
                }
            }
        }

        public ITextDataRoot Read(Stream stream, Encoding encoding) 
            => ReadStructuredData(CreateAdapter(stream, encoding), _comparer);

        public ITextDataRoot Read(string data) => ReadStructuredData(CreateAdapter(data), _comparer);

        protected StringComparer Comparer => _comparer;
    }
}