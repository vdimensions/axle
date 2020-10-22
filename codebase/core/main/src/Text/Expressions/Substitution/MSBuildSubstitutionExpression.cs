namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// An implementation of the <see cref="ISubstitutionExpression"/> interface which
    /// uses MSBuild-style delimiters (<c>'$'(</c> and <c>')'</c>) to identify text replacement tokens.
    /// </summary>
    public sealed class MSBuildSubstitutionExpression : AbstractSubstitutionExpression
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MSBuildSubstitutionExpression"/> class.
        /// </summary>
        public MSBuildSubstitutionExpression() : base(@"$\(", @"\)"){}

        /// <inheritdoc />
        protected override string ExtractTokenValue(string token)
        {
            const string exprStart = "$(", exprEnd = ")";
            return token.Substring(exprStart.Length, token.Length - (exprStart.Length + exprEnd.Length));
        }
    }
}