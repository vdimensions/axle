using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    public sealed class DictionarySubstitutionProvider : ISubstitutionProvider
    {
        private readonly IDictionary<string, string> _dict;

        public DictionarySubstitutionProvider(IDictionary<string, string> dict)
        {
            _dict = dict.VerifyArgument(nameof(dict)).IsNotNull().Value;
        }

        public bool TrySubstitute(string token, out string value) => 
            _dict.TryGetValue(token.VerifyArgument(nameof(token)).IsNotNullOrEmpty().Value, out value);
    }
}