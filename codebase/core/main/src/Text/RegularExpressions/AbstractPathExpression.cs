#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    [Obsolete("Use `Axle.Text.Expressions.Path.AbstractPathExpression` instead")]
    public abstract class AbstractPathExpression : Axle.Text.Expressions.Path.AbstractPathExpression, IPathExpression
    {
        protected AbstractPathExpression(string pattern, Func<string, RegexOptions, Regex> regexFactory)
            : base(pattern, regexFactory) { }
    }
}
#endif