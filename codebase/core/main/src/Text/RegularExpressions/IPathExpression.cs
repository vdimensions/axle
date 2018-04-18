namespace Axle.Text.RegularExpressions
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