#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using Axle.Text.Expressions.Regular;


namespace Axle.Text.Expressions.Path
{
    /// <summary>
    /// A regular expressions designed for matching paths.
    /// </summary>
    /// <seealso cref="IRegularExpression"/>
    public interface IPathExpression : IRegularExpression
    {
        /// <summary>
        /// The expression pattern used to match paths against, when any of the <c>Split</c> or <c>IsMatch</c>
        /// method overloads is called.
        /// </summary>
        string Pattern { get; }
    }
}
#endif