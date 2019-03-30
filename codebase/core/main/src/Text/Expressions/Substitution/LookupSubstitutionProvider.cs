using System.Linq;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    public sealed class LookupSubstitutionProvider : ISubstitutionProvider
    {
        private readonly ILookup<string, string> _lookup;

        public LookupSubstitutionProvider(ILookup<string, string> lookup)
        {
            _lookup = lookup.VerifyArgument(nameof(lookup)).IsNotNull().Value;
        }

        public bool TrySubstitute(string token, out string value)
        {
            if (_lookup.Contains(token))
            {
                value = _lookup[token].LastOrDefault();
                return !string.IsNullOrEmpty(value);
            }

            value = token;
            return false;
        }
    }
}