using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Axle.Text.StructuredData
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract class AbstractStructuredDataReader : IStructuredDataReader
    {
        private static IEnumerable<IStructuredDataNode> FixHierarchy(string key, IEnumerable<IStructuredDataNode> nodes)
        {
            var tokenizedKey = StructuredDataObject.Tokenize(key);
            var currentNodes = nodes;
            if (tokenizedKey.Length > 1)
            {
                var fixedKey = tokenizedKey[tokenizedKey.Length - 1];
                currentNodes = currentNodes.Select(
                    node =>
                    {
                        switch (node)
                        {
                            case IStructuredDataValue v:
                                return new StructuredDataValue(fixedKey, null, v.Value);
                            case IStructuredDataObject o:
                                return new StructuredDataObject(fixedKey, null, o.GetChildren(), false);
                            default:
                                return node;
                        }
                    });
            }
            for (var i = tokenizedKey.Length - 2; i >= 0; i--)
            {
                currentNodes = new[] {new StructuredDataObject(tokenizedKey[i], null, currentNodes, false)};
            }
            return currentNodes;
        }
        
        private readonly StringComparer _comparer;

        protected AbstractStructuredDataReader(StringComparer comparer)
        {
            _comparer = comparer;
        }

        protected abstract IStructuredDataAdapter CreateAdapter(Stream stream, Encoding encoding);
        protected abstract IStructuredDataAdapter CreateAdapter(string data);
        
        private IStructuredDataRoot ReadStructuredData(
            IStructuredDataAdapter adapter, 
            StringComparer comparer)
        {
            return new StructuredDataObjectRoot(ReadStructuredData(string.Empty, adapter), comparer);
        }

        private IEnumerable<IStructuredDataNode> ExpandChildren(IStructuredDataAdapter adapter)
        {
            foreach (var childGroup in adapter.Children.GroupBy(x => x.Key))
            foreach (var child in childGroup)
            foreach (var node in FixHierarchy(childGroup.Key, ReadStructuredData(childGroup.Key, child)))
            {
                yield return node;
            }
        }
        
        private IEnumerable<IStructuredDataNode> ReadStructuredData(
            string key,
            IStructuredDataAdapter adapter)
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
                    yield return new StructuredDataValue(key, null, adapter.Value);
                }
                var children = ExpandChildren(adapter);
                if (children.Any())
                {
                    yield return new StructuredDataObject(key, null, children, false);
                }
            }
        }

        public IStructuredDataRoot Read(Stream stream, Encoding encoding) 
            => ReadStructuredData(CreateAdapter(stream, encoding), _comparer);

        public IStructuredDataRoot Read(string data) => ReadStructuredData(CreateAdapter(data), _comparer);

        protected StringComparer Comparer => _comparer;
    }
}