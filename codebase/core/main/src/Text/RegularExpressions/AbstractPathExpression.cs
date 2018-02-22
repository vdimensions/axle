using System;
using System.Text.RegularExpressions;

using Axle.Verification;


namespace Axle.Text.RegularExpressions
{
    #if !NETSTANDARD
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

        private readonly IRegularExpression Target;

        private AbstractPathExpression(IRegularExpression target)
        {
            Target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
        }
        protected AbstractPathExpression(string pattern, Func<string, RegexOptions, Regex> regexFactory) 
            : this(new RegularExpression(regexFactory(pattern, RegExOptions))) { }

        public bool IsMatch(string value) => Target.IsMatch(value);
        public bool IsMatch(string value, int startIndex) => Target.IsMatch(value, startIndex);

        public Match[] Match(string value) => Target.Match(value);
        public Match[] Match(string value, int startIndex) => Target.Match(value, startIndex);

        public string[] Split(string value) => Target.Split(value);
        public string[] Split(string value, int count) => Target.Split(value, count);
        public string[] Split(string value, int count, int startIndex) => Target.Split(value, count, startIndex);

        public override string ToString() => Pattern;

        public abstract string Pattern { get; }
    }
}