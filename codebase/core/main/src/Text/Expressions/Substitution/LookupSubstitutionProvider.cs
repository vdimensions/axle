using System.Linq;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    /// <summary>
    /// A <see cref="ISubstitutionProvider"/> implementation using a <see cref="ILookup{TKey,TElement}"/> as a source.
    /// Because a <see cref="ILookup{TKey,TElement}"/> may contain a collection of values under a certain key,
    /// the <see cref="LookupSubstitutionProvider"/> will only use the last value in that collection.
    /// </summary>
    public sealed class LookupSubstitutionProvider : ISubstitutionProvider
    {
        private readonly ILookup<string, string> _lookup;

        /// <summary>
        /// Creates a new instance of the <see cref="LookupSubstitutionProvider"/> class using the provided
        /// <paramref name="lookup"/>.
        /// </summary>
        /// <param name="lookup">
        /// A <see cref="ILookup{TKey,TElement}"/> containing the data to be used when resolving substitution tokens.
        /// </param>
        public LookupSubstitutionProvider(ILookup<string, string> lookup)
        {
            _lookup = Verifier.IsNotNull(Verifier.VerifyArgument(lookup, nameof(lookup))).Value;
        }

        /// <inheritdoc />
        public bool TrySubstitute(string token, out string value)
        {
            if (_lookup.Contains(token))
            {
                value = Enumerable.LastOrDefault(_lookup[token]);
                return value != null;
            }

            value = token;
            return false;
        }
    }
}