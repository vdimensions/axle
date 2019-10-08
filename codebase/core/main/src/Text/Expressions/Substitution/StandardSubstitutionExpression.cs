namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// An implementation of the <see cref="ISubstitutionExpression"/> interface that uses
    /// the standard token delimiters (<c>'${'</c> and <c>'}'</c>) to identify replacement tokens.
    /// </summary>
    public class StandardSubstitutionExpression : AbstractSubstitutionExpression
    {
        /// <summary>
        /// Creates a new instance of the <see cref="StandardSubstitutionExpression"/> class.
        /// </summary>
        public StandardSubstitutionExpression() : base("${", "}"){}
    }
}