namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// An implementation of the <see cref="ISubstitutionExpression"/> interface which
    /// uses MSBuild-style delimiters (<c>'$'(</c> and <c>')'</c>) to identify text replacement tokens.
    /// </summary>
    public class MSBuildSubstitutionExpression : AbstractSubstitutionExpression
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MSBuildSubstitutionExpression"/> class.
        /// </summary>
        public MSBuildSubstitutionExpression() : base("$(", ")"){}
    }
}