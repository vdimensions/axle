namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// An interface representing a substitution expression,
    /// which can be used to replace values a given text input
    /// that match specific rules.
    /// </summary>
    public interface ISubstitutionExpression
    {
        /// <summary>
        /// Replaces values from the provided <paramref name="input"/> text using the rules specified by the given <see cref="ISubstitutionProvider"/>. 
        /// </summary>
        /// <param name="input">
        /// The input text to be replaced.
        /// </param>
        /// <param name="sp">
        /// A <see cref="ISubstitutionProvider"/> instance that provides the substitution values for the elements of the given text that are to be substituted.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> containing the result from the replacement operation.
        /// </returns>
        string Replace(string input, ISubstitutionProvider sp);
    }
}