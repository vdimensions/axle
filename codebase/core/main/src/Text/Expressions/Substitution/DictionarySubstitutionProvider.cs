using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// An implementation of the <see cref="ISubstitutionProvider"/> interface that uses <see cref="IDictionary{TKey,TValue}"/>
    /// to represent the values to replace and the respective replacement text.
    /// </summary>
    public sealed class DictionarySubstitutionProvider : ISubstitutionProvider
    {
        private readonly IDictionary<string, string> _dictionary;

        /// <summary>
        /// Creates a new instance of the <see cref="DictionarySubstitutionProvider"/> class using the provided <paramref name="dictionary"/>.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary to supply the text replacement data.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="dictionary"/> is <c>null</c>.
        /// </exception>
        public DictionarySubstitutionProvider(IDictionary<string, string> dictionary)
        {
            _dictionary = Verifier.IsNotNull(Verifier.VerifyArgument(dictionary, nameof(dictionary))).Value;
        }

        /// <inheritdoc />
        public bool TrySubstitute(string token, out string value) => 
            _dictionary.TryGetValue(StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(token, nameof(token))).Value, out value);
    }
}