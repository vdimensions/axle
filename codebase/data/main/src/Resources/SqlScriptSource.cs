using System;
using System.Collections.Generic;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Data.Resources
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
            Name = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            _scripts = scripts.VerifyArgument(nameof(scripts)).IsNotNull().Value;
        }

        public string GetScript(string dialectName)
        {
            if (_scripts.TryGetValue(dialectName.VerifyArgument(nameof(dialectName)).IsNotNullOrEmpty(), out var result) || _scripts.TryGetValue(string.Empty, out result))
            {
                return result;
            }
            throw new NotSupportedException($"The '{Name}' script is not available for dialect '{dialectName}'");
        }

        public string Name { get; }
    }
}