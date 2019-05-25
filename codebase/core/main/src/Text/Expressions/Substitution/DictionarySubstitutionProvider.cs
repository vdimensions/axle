using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    public sealed class DictionarySubstitutionProvider : ISubstitutionProvider
    {
        private readonly IDictionary<string, string> _dict;

        public DictionarySubstitutionProvider(IDictionary<string, string> dict)
        {
            _dict = Verifier.IsNotNull(Verifier.VerifyArgument(dict, nameof(dict))).Value;
        }

        public bool TrySubstitute(string token, out string value) => 
            _dict.TryGetValue(StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(token, nameof(token))).Value, out value);
    }
}