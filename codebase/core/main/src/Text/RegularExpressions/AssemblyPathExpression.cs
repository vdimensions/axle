using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Axle.Verification;


namespace Axle.Text.RegularExpressions
{
    /// <summary>
    /// An implementation of the <see cref="IPathExpression" /> interface that is used to match paths for
    /// assemblies or embedded resources inside assemblies.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class AssemblyPathExpression : AbstractPathExpression
    {
        private static readonly IDictionary<string, string> _Replacements = new Dictionary<string, string>(3)
        {
            {@"\", @"."},
            {@"/", @"."},
            {@" ", @"_"}
        };

        private const string AntSingleAsteriskWinRegex = @"(?<=[\.]{0,1})(?:[^\.]+)(?=[\.]{0,1})";
        private const string AntDoubleAsteriskRegex = @"(?:.{0,})";

        private static Regex CreateRegex(string pattern, RegexOptions options)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var res = _Replacements.Aggregate(EscapeRegex(pattern), (p, c) => p.Replace(EscapeRegex(c.Key), EscapeRegex(c.Value)));
            //options |= RegexOptions.IgnoreCase;
            res = res.Replace("**", AntDoubleAsteriskRegex).Replace("*", AntSingleAsteriskWinRegex);
            return new Regex(res, options);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _pattern;

        public AssemblyPathExpression(string pattern) : base(pattern, CreateRegex)
        {
            var ptrn = _Replacements.Aggregate(pattern, (p, c) => p.Replace(c.Key, c.Value));
            _pattern = ptrn;
        }

        public bool IsMatch(Assembly value) => IsMatch(value.VerifyArgument(nameof(value)).IsNotNull().Value.GetName().Name);
        public bool IsMatch(AssemblyName value) => IsMatch(value.VerifyArgument(nameof(value)).Value.Name);

        public override string Pattern => _pattern;
    }
}