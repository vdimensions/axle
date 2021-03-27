#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
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
    /// <summary>
    /// An abstract class to aid the implementation of the <see cref="ISubstitutionExpression"/> interface.
    /// </summary>
    public abstract class AbstractSubstitutionExpression : ISubstitutionExpression
    {
        private const string MatchGroupName = "exp";

        private readonly string _exprStart;
        private readonly string _exprEnd;

        private const RegexOptions Options = 
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            RegexOptions.Compiled|
            #endif
            RegexOptions.CultureInvariant|RegexOptions.IgnoreCase|RegexOptions.Multiline|RegexOptions.ExplicitCapture;

        private readonly Regex _expression;
        private readonly Regex _fallbackSplitExpression = new Regex(@"((?:(?<!\\)\:)+)", Options);

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

        /// <summary>
        /// When overriden in a derived class, creates a new instance of the
        /// respective <see cref="AbstractSubstitutionExpression"/> implementation using the provided substitution
        /// expression delimiters.
        /// </summary>
        /// <param name="exprStart">
        /// The delimiter signifying the beginning of a substitution expression.
        /// </param>
        /// <param name="exprEnd">
        /// The delimiter signifying the end of a substitution expression.
        /// </param>
        protected AbstractSubstitutionExpression(string exprStart, string exprEnd)
        {
            _exprStart = exprStart;
            _exprEnd = exprEnd;
            var regExPattern = @"(?<" + MatchGroupName + @">(?:\"+_exprStart+")(?:<" + MatchGroupName + @">|(?:[^"+_exprEnd+"])+)+(?:"+_exprEnd+"))";
            _expression = new Regex(regExPattern, Options);
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
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
                    var nakedToken = ExtractTokenValue(token);
                    var replacementStartIndex = result.IndexOf(token, StringComparison.Ordinal);
                    var resolved = false;
                    if (replacementStartIndex > 0)
                    {
                        resolved = false;
                    }
                    var tokenAndFallbacks = GetTokenAndFallbacks(nakedToken);
                    foreach (var tokenValue in tokenAndFallbacks)
                    {
                        if (sp.TrySubstitute(tokenValue, out var replacement))
                        {
                            result = ReplaceToken(result, replacement, replacementStartIndex, token);
                            resolved = true;
                            break;
                        }
                    }
                    if (!resolved)
                    {
                        if (tokenAndFallbacks.Length > 1)
                        {
                            result = ReplaceToken(result, tokenAndFallbacks[tokenAndFallbacks.Length - 1], replacementStartIndex, token);
                            resolved = true;
                        }
                        else
                        {
                            #if NETSTANDARD || NET35_OR_NEWER
                            unresolved.Add(token);
                            #else
                            unresolved.Add(token, token);
                            #endif
                        }
                    }
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


        protected virtual string ExtractTokenValue(string token) => token.Substring(_exprStart.Length, token.Length - (_exprStart.Length + _exprEnd.Length));

        private static string ReplaceToken(string text, string replacement, int replacementStartIndex, string token)
        {
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, replacementStartIndex));
            sb.Append(replacement);
            sb.Append(text.Substring(replacementStartIndex + token.Length));
            return sb.ToString();
        }

        protected virtual string[] GetTokenAndFallbacks(string nakedToken)
        {
            return Enumerable.ToArray(
                    Enumerable.Select(
                        _fallbackSplitExpression.Split(nakedToken), 
                        x => x.Replace(@"\:", ":")));
        }
    }
}
#endif