using System;
using System.Text.RegularExpressions;

using Axle.Verification;


namespace Axle.Text.RegularExpressions
{
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
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
            foreach (var terminal in EscapedRegexTerminals)
            {
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

        public bool IsMatch(string value) => _target.IsMatch(value);
        public bool IsMatch(string value, int startIndex) => _target.IsMatch(value, startIndex);

        public Match[] Match(string value) => _target.Match(value);
        public Match[] Match(string value, int startIndex) => _target.Match(value, startIndex);

        public string[] Split(string value) => _target.Split(value);
        public string[] Split(string value, int count) => _target.Split(value, count);
        public string[] Split(string value, int count, int startIndex) => _target.Split(value, count, startIndex);

        public override string ToString() => Pattern;

        public abstract string Pattern { get; }
    }
}