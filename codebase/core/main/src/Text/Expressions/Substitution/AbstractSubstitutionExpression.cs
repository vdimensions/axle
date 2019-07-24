#if NETSTANDARD || NET20_OR_NEWER
using System;
#if NETSTANDARD || NET35_OR_NEWER
using System.Collections.Generic;
#else
using System.Collections;
#endif
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Axle.Verification;


namespace Axle.Text.Expressions.Substitution
{
    public abstract class AbstractSubstitutionExpression : ISubstitutionExpression
    {
        private const string MatchGroupName = "exp";

        private readonly string _exprStart;
        private readonly string _exprEnd;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        private const RegexOptions Options = RegexOptions.Compiled|RegexOptions.CultureInvariant|RegexOptions.IgnoreCase|RegexOptions.Multiline|RegexOptions.ExplicitCapture;
        #else
        private const RegexOptions Options = RegexOptions.CultureInvariant|RegexOptions.IgnoreCase|RegexOptions.Multiline|RegexOptions.ExplicitCapture;
        #endif

        private readonly Regex _expression;

        private static string[] GetCaptures(Regex regex, string input, StringComparer comparer)
        {
            var groups = regex.Match(input).Groups;
            return Enumerable.ToArray(
                Enumerable.Distinct(
                    Enumerable.Select(
                        Enumerable.SelectMany(
                            Enumerable.ToDictionary(
                                Enumerable.Where(
                                    regex.GetGroupNames(),
                                    groupName => groups[groupName].Captures.Count > 0),
                                groupName => groupName,
                                groupName => groups[groupName]),
                            x => Enumerable.Cast<Capture>(x.Value.Captures)),
                        x => x.Value),
                    comparer));
        }

        protected AbstractSubstitutionExpression(string exprStart, string exprEnd)
        {
            _exprStart = exprStart;
            _exprEnd = exprEnd;
            var regExPattern = @"(?<" + MatchGroupName + @">(?:\"+_exprStart+")(?:<" + MatchGroupName + @">|(?:[^"+_exprEnd+"])+)+(?:"+_exprEnd+"))";
            _expression = new Regex(regExPattern, Options);
        }

        public string Replace(string input, ISubstitutionProvider sp)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(sp, nameof(sp)));
            Verifier.IsNotNull(Verifier.VerifyArgument(input, nameof(input)));
            var cmp = StringComparer.Ordinal;
            var result = input;
            var groupCaptures = GetCaptures(_expression, result, cmp);
            #if NETSTANDARD || NET35_OR_NEWER
            var unresolved = new HashSet<string>(cmp);
            #else
            var unresolved = new Hashtable(cmp);
            #endif
            do
            {
                foreach (var token in groupCaptures)
                {
                    var nakedToken = token.Substring(_exprStart.Length, token.Length - (_exprStart.Length + _exprEnd.Length));
                    var ix = result.IndexOf(token, StringComparison.Ordinal);
                    if (ix < 0 || !sp.TrySubstitute(nakedToken, out var replacement))
                    {
                        #if NETSTANDARD || NET35_OR_NEWER
                        unresolved.Add(token);
                        #else
                        unresolved.Add(token, token);
                        #endif
                        continue;
                    }

                    var sb = new StringBuilder();
                    sb.Append(result.Substring(0, ix));
                    sb.Append(replacement);
                    sb.Append(result.Substring(ix + token.Length));
                    result = sb.ToString();
                }

                groupCaptures = GetCaptures(_expression, result, cmp);
            }
            #if NETSTANDARD || NET35_OR_NEWER
            while (Enumerable.Any(Enumerable.Except(groupCaptures, unresolved, cmp)));
            #else
            while (Enumerable.Any(Enumerable.Except(groupCaptures, Enumerable.Cast<string>(unresolved.Keys), cmp)));
            #endif

            return result;
        }
    }
}
#endif