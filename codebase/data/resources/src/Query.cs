using System;
using System.Collections.Generic;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Data.Resources
{
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
    [Serializable]
    #endif
    public sealed class Query
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<string, string> _scripts;

        internal Query(string name, IDictionary<string, string> scripts)
        {
            Name = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            _scripts = scripts.VerifyArgument(nameof(scripts)).IsNotNull().Value;
        }

        //public string GetScript(DataSource dataSource)
        //{
        //    return GetScript(dataSource.VerifyArgument(nameof(dataSource)).IsNotNull().Value.Dialect);
        //}
        public string GetScript(string dialectName)
        {
            if (_scripts.TryGetValue(dialectName.VerifyArgument(nameof(dialectName)).IsNotNullOrEmpty(), out var result) || _scripts.TryGetValue(string.Empty, out result))
            {
                return result;
            }
            throw new NotSupportedException($"The query '{Name}' is not available for dialect '{dialectName}'");
        }

        public string Name { get; }
    }
}