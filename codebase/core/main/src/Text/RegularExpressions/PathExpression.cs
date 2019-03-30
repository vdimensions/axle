#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    [Obsolete("Use `Axle.Text.Expressions.Path.PathExpression` instead")]
    public sealed class PathExpression : IPathExpression
    {
        private readonly Axle.Text.Expressions.Path.PathExpression _expr;

        public PathExpression(string pattern)
        {
            _expr = new Axle.Text.Expressions.Path.PathExpression(pattern);
        }

        public bool IsMatch(string input) => _expr.IsMatch(input);
        public bool IsMatch(string input, int startIndex) => _expr.IsMatch(input, startIndex);

        public Match[] Match(string input) => _expr.Match(input);
        public Match[] Match(string input, int startIndex) => _expr.Match(input, startIndex);

        public string[] Split(string input) => _expr.Split(input);
        public string[] Split(string input, int count) => _expr.Split(input, count);
        public string[] Split(string input, int count, int startIndex) => _expr.Split(input, count, startIndex);

        public string Pattern => _expr.Pattern;
    }
}
#endif