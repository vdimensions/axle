#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Text.RegularExpressions;

using Axle.Verification;
using Axle.Text.Expressions.Regular;


namespace Axle.Text.Expressions.Path
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractPathExpression : IPathExpression
    {
        protected const RegexOptions RegExOptions = RegexOptions.CultureInvariant;
        //private const string EscapedRegexTerminals = @"\!:?.&=-+%[](){}^$";
        private const string EscapedRegexTerminals = @"\!:?.&=%^$";

        protected static string EscapeRegex(string value)
        {
            // ReSharper disable LoopCanBeConvertedToQuery
            var result = value;
            for (var i = 0; i < EscapedRegexTerminals.Length; i++)
            {
                var terminal = EscapedRegexTerminals[i];
                result = result.Replace(terminal.ToString(), @"\" + terminal);
            }
            return result;
            // ReSharper restore LoopCanBeConvertedToQuery
        }

        private readonly IRegularExpression _target;

        private AbstractPathExpression(IRegularExpression target)
        {
            _target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
        }
        protected AbstractPathExpression(string pattern, Func<string, RegexOptions, Regex> regexFactory) 
            : this(new RegularExpression(regexFactory(pattern, RegExOptions))) { }

        public bool IsMatch(string input) => _target.IsMatch(input);
        public bool IsMatch(string input, int startIndex) => _target.IsMatch(input, startIndex);

        public Match[] Match(string input) => _target.Match(input);
        public Match[] Match(string input, int startIndex) => _target.Match(input, startIndex);

        public string[] Split(string input) => _target.Split(input);
        public string[] Split(string input, int count) => _target.Split(input, count);
        public string[] Split(string input, int count, int startIndex) => _target.Split(input, count, startIndex);

        public override string ToString() => Pattern;

        public abstract string Pattern { get; }
    }
}
#endif