#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
    /// <summary>
    /// An implementation of the <see cref="IPathExpression" /> interface that is used to match paths for
    /// assemblies or embedded resources inside assemblies.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    [Obsolete("Use `Axle.Text.Expressions.Path.AssemblyPathExpression` instead")]
    public sealed class AssemblyPathExpression : IPathExpression
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Axle.Text.Expressions.Path.AssemblyPathExpression _expr;

        public AssemblyPathExpression(string pattern)
        {
             _expr = new Axle.Text.Expressions.Path.AssemblyPathExpression(pattern);
        }

        public bool IsMatch(Assembly value) => _expr.IsMatch(value);
        public bool IsMatch(AssemblyName value) => _expr.IsMatch(value);
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