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

        private IEnumerable<IStructuredDataNode> FixHierarchy(string key, IEnumerable<IStructuredDataNode> nodes)
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
        private IList<IStructuredDataNode> ReadStructuredData(
            string key,
            IStructuredDataAdapter adapter)
        {
            var result = new List<IStructuredDataNode>();
            if (adapter.Value != null)
            {
                result.Add(new StructuredDataValue(key, null, adapter.Value));
            }
            var childrenDictionary = adapter.GetChildren();
            foreach (var nestedData in childrenDictionary)
            foreach (var values in nestedData.Value.Select(v => ReadStructuredData(nestedData.Key, v)))
            {
                if (key.Length == 0)
                {
                    foreach (var node in FixHierarchy(nestedData.Key, values))
                    {
                        result.Add(node);
                    }
                }
                else
                {
                    var nodes = new List<IStructuredDataNode>();
                    foreach (var node in FixHierarchy(nestedData.Key, values))
                    {
                        nodes.Add(node);
                    }
                    result.Add(new StructuredDataObject(key, null, nodes));
                }
            }
            return result;
        }
        
        public IStructuredDataRoot Read(Stream stream, Encoding encoding) 
            => ReadStructuredData(CreateAdapter(stream, encoding), _comparer);

        public IStructuredDataRoot Read(string data) => ReadStructuredData(CreateAdapter(data), _comparer);

        protected StringComparer Comparer => _comparer;
    }
}