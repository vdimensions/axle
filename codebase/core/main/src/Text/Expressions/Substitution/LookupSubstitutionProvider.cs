#if NETSTANDARD || NET35_OR_NEWER
using System.Linq;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    public sealed class LookupSubstitutionProvider : ISubstitutionProvider
    {
        private readonly ILookup<string, string> _lookup;

        public LookupSubstitutionProvider(ILookup<string, string> lookup)
        {
            _lookup = Verifier.IsNotNull(Verifier.VerifyArgument(lookup, nameof(lookup))).Value;
        }

        public bool TrySubstitute(string token, out string value)
        {
            if (_lookup.Contains(token))
            {
                value = Enumerable.LastOrDefault(_lookup[token]);
                return !string.IsNullOrEmpty(value);
            }

            value = token;
            return false;
        }
    }
}
#endif