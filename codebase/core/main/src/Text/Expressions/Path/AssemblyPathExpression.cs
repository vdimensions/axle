using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Axle.Verification;

namespace Axle.Text.Expressions.Path
{
    /// <summary>
    /// An implementation of the <see cref="IPathExpression" /> interface that is used to match paths for
    /// assemblies or embedded resources inside assemblies.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class AssemblyPathExpression : AbstractPathExpression
    {
        private static readonly IDictionary<string, string> Replacements = new Dictionary<string, string>(3, StringComparer.Ordinal)
        {
            {@"\", @"."},
            {@"/", @"."},
            {@" ", @"_"}
        };

        private const string SingleAsteriskGlobWinRegex = @"(?<=[\.]{0,1})(?:[^\.]+)(?=[\.]{0,1})";
        private const string DoubleAsteriskGlobRegex = @"(?:.{0,})";

        private static Regex CreateRegex(string pattern, RegexOptions options)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var res = Enumerable.Aggregate(Replacements, EscapeRegex(pattern), (p, c) => p.Replace(EscapeRegex(c.Key), EscapeRegex(c.Value)));
            //options |= RegexOptions.IgnoreCase;
            res = res.Replace("**", DoubleAsteriskGlobRegex).Replace("*", SingleAsteriskGlobWinRegex);
            return new Regex(res, options);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _pattern;

        /// <summary>
        /// Creates a new instance of the <see cref="AssemblyPathExpression"/> class using the provided <paramref name="pattern"/>.
        /// </summary>
        /// <param name="pattern">
        /// The pattern that to match.
        /// </param>
        public AssemblyPathExpression(string pattern) : base(pattern, CreateRegex)
        {
            var processedPattern = Enumerable.Aggregate(Replacements, pattern, (p, c) => p.Replace(c.Key, c.Value));
            _pattern = processedPattern;
        }
        /// <summary>
        /// Indicates whether the provided <paramref name="value"/> matches against the current <see cref="AssemblyPathExpression"/>'s pattern.
        /// </summary>
        /// <param name="value">
        /// The value to match against the current <see cref="AssemblyPathExpression"/> instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the provided <see cref="Assembly"/> <paramref name="value"/> matches against the current <see cref="AssemblyPathExpression"/>'s pattern;
        /// <c>false</c> otherwise.
        /// </returns>
        public bool IsMatch(Assembly value) => IsMatch(Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value))).Value.GetName().Name);
        /// <summary>
        /// Indicates whether the provided <see cref="AssemblyName"/> <paramref name="value"/> matches against the current <see cref="AssemblyPathExpression"/>'s pattern.
        /// </summary>
        /// <param name="value">
        /// The value to match against the current <see cref="AssemblyPathExpression"/> instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the provided <paramref name="value"/> matches against the current <see cref="AssemblyPathExpression"/>'s pattern;
        /// <c>false</c> otherwise.
        /// </returns>
        public bool IsMatch(AssemblyName value) => IsMatch(Verifier.VerifyArgument(value, nameof(value)).Value.Name);

        /// <inheritdoc />
        public override string Pattern => _pattern;
    }
}