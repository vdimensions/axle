using System;
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

        private static Capture[] GetCaptures(Regex regex, string input)
        {
            var groups = regex.Match(input).Groups;
            return regex.GetGroupNames()
                .Where(groupName => groups[(string) groupName].Captures.Count > 0).ToDictionary(groupName => groupName, groupName => groups[groupName])
                .SelectMany(x => x.Value.Captures.Cast<Capture>())
                .ToArray();
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
            sp.VerifyArgument(nameof(sp)).IsNotNull();
            input.VerifyArgument(nameof(input)).IsNotNull();
            var result = input;
            var groupCaptures = GetCaptures(_expression, result);
            do
            {
                foreach (var groupCapture in groupCaptures)
                {
                    var token = groupCapture.Value;
                    var nakedToken = token.Substring(_exprStart.Length, token.Length - (_exprStart.Length + _exprEnd.Length));
                    var ix = result.IndexOf(token, StringComparison.Ordinal);
                    if (ix < 0 || !sp.TrySubstitute(nakedToken, out var replacement))
                    {
                        continue;
                    }

                    var sb = new StringBuilder();
                    sb.Append(result.Substring(0, ix));
                    sb.Append(replacement);
                    sb.Append(result.Substring(ix + token.Length));
                    result = sb.ToString();
                }

                groupCaptures = GetCaptures(_expression, result);
            }
            while (groupCaptures.Length > 0);

            return result;
        }
    }
}