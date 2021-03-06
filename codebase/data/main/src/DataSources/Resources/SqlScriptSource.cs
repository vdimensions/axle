﻿using System;
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
            Name = StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(name, nameof(name)));
            _scripts = Verifier.IsNotNull(Verifier.VerifyArgument(scripts, nameof(scripts))).Value;
        }

        private string GetScript(string dialectName)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(dialectName, nameof(dialectName)));
            if (_scripts.TryGetValue(dialectName, out var result) || _scripts.TryGetValue(string.Empty, out result))
            {
                return result;
            }
            throw new NotSupportedException($"The '{Name}' script is not available for dialect '{dialectName}'");
        }

        public string ResolveScript(IDbServiceProvider dbProvider)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(dbProvider, nameof(dbProvider)));
            return GetScript(dbProvider.DialectName);
        }

        public string Name { get; }
    }
}