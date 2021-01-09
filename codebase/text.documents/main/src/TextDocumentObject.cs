using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Axle.Text.Expressions.Regular;

namespace Axle.Text.Documents
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    internal sealed class TextDocumentObject : TextDocumentNode, ITextDocumentObject
    {
        private const string NameTokenRegexPattern =
            "(?:(?:(?<=\\[)(?:[^\\]]+)(?=\\]))|(?:(?<=\\\")(?:[^\\\"]+)(?=\\\"))|(?:(?<=\\')(?:[^\\']+)(?=\\'))|(?:[^\\.\\n\\[\\]\\\"\\'\\:\\s]+))";

        private static readonly IRegularExpression _NameTokenRegex;

        static TextDocumentObject()
        {
            _NameTokenRegex = new RegularExpression(
                NameTokenRegexPattern, 
                #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
                RegexOptions.Compiled|
                #endif
                RegexOptions.CultureInvariant|RegexOptions.IgnoreCase);
        }

        internal static string[] Tokenize(string name)
        {
            return _NameTokenRegex.Match(name).Select(x => x.Value).ToArray();
        }
        
        #if NETSTANDARD2_0_OR_NEWER || NET30_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.Serialization.DataMember]
        #endif
        private readonly IEnumerable<ITextDocumentNode> _children;

        internal TextDocumentObject(
                string key, 
                ITextDocumentObject parent, 
                IEnumerable<ITextDocumentNode> children, 
                bool recursiveParentMapping = false) : base(key, parent)
        {
            var p = this;
            _children = recursiveParentMapping 
                ? children.Select(
                    child =>
                    {
                        switch (child)
                        {
                            case ITextDocumentValue value:
                                return new TextDocumentValue(value.Key, p, value.Value);
                            case ITextDocumentObject obj:
                                return new TextDocumentObject(obj.Key, p, obj.GetChildren(), true);
                            default:
                                return child;
                        }
                    })
                : children;
        }

        public IEnumerable<ITextDocumentNode> GetChildren() => _children;
        public IEnumerable<ITextDocumentNode> GetValues(string name)
        {
            var directMatches = GetChildrenByTokens(new[]{name}, 0, _children);
            if (directMatches.Length > 0)
            {
                return directMatches;
            }
            var tokens = Tokenize(name);
            return GetChildrenByTokens(tokens, 0, _children);
        }

        private ITextDocumentNode[] GetChildrenByTokens(string[] tokens, int startIndex, IEnumerable<ITextDocumentNode> children)
        {
            var comparer = ((ITextDocumentNode) this).KeyComparer;
            while (true)
            {
                var token = tokens[startIndex];
                var matchingChildren = children.Where(child => comparer.Equals(child.Key, token));
                if ((startIndex + 1) == tokens.Length)
                {
                    return matchingChildren.ToArray();
                }
                startIndex = 1 + startIndex;
                children = matchingChildren.OfType<ITextDocumentObject>() .SelectMany(c => c.GetChildren());
            }
        }
    }
}
