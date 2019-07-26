using System;
using System.Collections.Generic;
using System.Diagnostics;
using Axle.Verification;

namespace Axle.Data.DataSources.Resources
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class SqlScriptSource
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<string, string> _scripts;

        internal SqlScriptSource(string name, IDictionary<string, string> scripts)
        {
            this.Name = StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(name, nameof(name)));
            _scripts = Verifier.IsNotNull(Verifier.VerifyArgument(scripts, nameof(scripts))).Value;
        }

        private string GetScript(string dialectName)
        {
            if (_scripts.TryGetValue(dialectName.VerifyArgument(nameof(dialectName)).IsNotNullOrEmpty(), out var result) || _scripts.TryGetValue(string.Empty, out result))
            {
                return result;
            }
            throw new NotSupportedException($"The '{Name}' script is not available for dialect '{dialectName}'");
        }

        public string ResolveScript(IDbServiceProvider dbProvider)
        {
            dbProvider.VerifyArgument(nameof(dbProvider)).IsNotNull();
            return GetScript(dbProvider.DialectName);
        }

        public string Name { get; }
    }
}