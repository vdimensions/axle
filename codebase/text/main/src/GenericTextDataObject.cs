using System.Collections.Generic;

namespace Axle.Text
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class GenericTextDataObject : ITextDataObject
    {
        private readonly IEnumerable<ITextDataNode> _children;

        public GenericTextDataObject(string name, IEnumerable<ITextDataNode> children)
        {
            Name = name;
            _children = children;
        }

        public IEnumerable<ITextDataNode> GetChildren() => _children;

        public IEnumerable<ITextDataNode> GetChildren(string name)
        {
            // TODO: comparer
            return null;
        }

        public string Name { get; }
    }
}
