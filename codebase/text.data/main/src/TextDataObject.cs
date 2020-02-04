using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Axle.Text.Expressions.Regular;

namespace Axle.Text.Data
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class TextDataObject : TextDataNode, ITextDataObject
    {
        private const string NameTokenRegexPattern =
            "(?:(?:(?<=\\[)(?:[^\\]]+)(?=\\]))|(?:(?<=\\\")(?:[^\\\"]+)(?=\\\"))|(?:(?<=\\')(?:[^\\']+)(?=\\'))|(?:[^\\.\\n\\[\\]\\\"\\'\\:\\s]+))";

        private static readonly IRegularExpression _NameTokenRegex;

        static TextDataObject()
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
        private readonly IEnumerable<ITextDataNode> _children;
        
        public TextDataObject(
                string key, 
                ITextDataObject parent, 
                IEnumerable<ITextDataNode> children, 
                bool recursiveParentMapping = false) : base(key, parent)
        {
            var p = this;
            _children = recursiveParentMapping 
                ? children.Select(
                    child =>
                    {
                        switch (child)
                        {
                            case ITextDataValue value:
                                return new TextDataValue(value.Key, p, value.Value);
                            case ITextDataObject obj:
                                return new TextDataObject(obj.Key, p, obj.GetChildren(), true);
                            default:
                                return child;
                        }
                    })
                : children;
        }

        public TextDataObject(
                string key, 
                ITextDataObject parent, 
                IEnumerable<ITextDataNode> children) 
            : this(key, parent, children, true) { }

        public IEnumerable<ITextDataNode> GetChildren() => _children;
        public IEnumerable<ITextDataNode> GetChildren(string name)
        {
            var directMatches = GetChildrenByTokens(new[]{name}, 0, _children);
            if (directMatches.Length > 0)
            {
                return directMatches;
            }
            var tokens = Tokenize(name);
            return GetChildrenByTokens(tokens, 0, _children);
        }

        private ITextDataNode[] GetChildrenByTokens(string[] tokens, int startIndex, IEnumerable<ITextDataNode> children)
        {
            var comparer = ((ITextDataNode) this).KeyComparer;
            while (true)
            {
                var token = tokens[startIndex];
                var matchingChildren = children.Where(child => comparer.Equals(child.Key, token));
                if ((startIndex + 1) == tokens.Length)
                {
                    return matchingChildren.ToArray();
                }
                startIndex = 1 + startIndex;
                children = matchingChildren.OfType<ITextDataObject>() .SelectMany(c => c.GetChildren());
            }
        }
    }
}
