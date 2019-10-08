namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// An interface representing a text substitution provider.
    /// Text substitution providers are usually used in conjunction with a <see cref="ISubstitutionExpression"/> object,
    /// where the former provide text replacement information used by the <see cref="ISubstitutionExpression.Replace"/> method.
    /// </summary>
    public interface ISubstitutionProvider
    {
        /// <summary>
        /// Attempts to fetch a substitution <paramref name="value"/> for the provided text <paramref name="token"/>.
        /// </summary>
        /// <param name="token">
        /// The text token to be substituted.
        /// </param>
        /// <param name="value">
        /// Contains the replacement value if the current <see cref="ISubstitutionProvider"/> instance finds a suitable
        /// replacement for the provided <paramref name="token"/>. 
        /// <para>This parameter is passed uninitialized.</para>
        /// </param>
        /// <returns>
        /// <c>true</c> if the current <see cref="ISubstitutionProvider"/> instance successfully finds a substitution value for the
        /// passed in <paramref name="token"/>;
        /// <c>false</c> otherwise.
        /// </returns>
        bool TrySubstitute(string token, out string value);
    }
}