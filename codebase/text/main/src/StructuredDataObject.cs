using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Axle.Text.Expressions.Regular;

namespace Axle.Text.StructuredData
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class StructuredDataObject : StructuredDataNode, IStructuredDataObject
    {
        private const string NameTokenRegexPattern =
            "(?:(?:(?<=\\[)(?:[^\\]]+)(?=\\]))|(?:(?<=\\\")(?:[^\\\"]+)(?=\\\"))|(?:(?<=\\')(?:[^\\']+)(?=\\'))|(?:[^\\.\\n\\[\\]\\\"\\'\\:\\s]+))";

        private static readonly IRegularExpression _NameTokenRegex;

        static StructuredDataObject()
        {
            _NameTokenRegex = new RegularExpression(
                NameTokenRegexPattern, 
                #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
                RegexOptions.Compiled|
                #endif
                RegexOptions.CultureInvariant|RegexOptions.IgnoreCase);
        }

        internal static string[] Tokenize(string name)
        {
            return _NameTokenRegex.Match(name).Select(x => x.Value).ToArray();
        }
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly IEnumerable<IStructuredDataNode> _children;
        
        public StructuredDataObject(
                string key, 
                IStructuredDataObject parent, 
                IEnumerable<IStructuredDataNode> children, 
                bool recursiveParentMapping = false) : base(key, parent)
        {
            var p = this;
            _children = recursiveParentMapping 
                ? children.Select(
                    child =>
                    {
                        switch (child)
                        {
                            case StructuredDataValue value:
                                return new StructuredDataValue(value.Key, p, value.Value);
                            case StructuredDataObject obj:
                                return new StructuredDataObject(obj.Key, p, obj.GetChildren(), true);
                            default:
                                return child;
                        }
                    })
                : children;
        }

        public StructuredDataObject(
                string key, 
                IStructuredDataObject parent, 
                IEnumerable<IStructuredDataNode> children) 
            : this(key, parent, children, true) { }

        public IEnumerable<IStructuredDataNode> GetChildren() => _children;
        public IEnumerable<IStructuredDataNode> GetChildren(string name)
        {
            var directMatches = GetChildrenByTokens(new[]{name}, 0, _children);
            if (directMatches.Length > 0)
            {
                return directMatches;
            }
            var tokens = Tokenize(name);
            return GetChildrenByTokens(tokens, 0, _children);
        }

        private IStructuredDataNode[] GetChildrenByTokens(string[] tokens, int startIndex, IEnumerable<IStructuredDataNode> children)
        {
            var comparer = ((IStructuredDataNode) this).KeyComparer;
            while (true)
            {
                var token = tokens[startIndex];
                var matchingChildren = children.Where(child => comparer.Equals(child.Key, token));
                if ((startIndex + 1) == tokens.Length)
                {
                    return matchingChildren.ToArray();
                }
                startIndex = 1 + startIndex;
                children = matchingChildren.OfType<IStructuredDataObject>() .SelectMany(c => c.GetChildren());
            }
        }
    }
}
