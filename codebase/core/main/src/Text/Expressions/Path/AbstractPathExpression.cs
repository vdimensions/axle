#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Text.RegularExpressions;

using Axle.Verification;
using Axle.Text.Expressions.Regular;


namespace Axle.Text.Expressions.Path
{
    /// <summary>
    /// An abstract class aiding the implementation of the <see cref="IPathExpression"/> interface.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractPathExpression : IPathExpression
    {
        protected const RegexOptions RegExOptions = RegexOptions.CultureInvariant;
        //private const string EscapedRegexTerminals = @"\!:?.&=-+%[](){}^$";
        private const string EscapedRegexTerminals = @"\!:?.&=%^$";

        /// <summary>
        /// A helper function that is used to escape a given <see cref="string"/> <paramref name="value"/> in order for it to 
        /// become a valid regex expression.
        /// </summary>
        /// <param name="value">
        /// The <see cref="string"/> value to escape.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> instance prduced from the <paramref name="value"/> parameter by escaping 
        /// all special regex symbols that were present in the original value. The resulting string is therefore
        /// suitable to be used as a pattern when creating a <see cref="Regex"/> object.
        /// </returns>
        /// <seealso cref="Regex"/>
        /// <seealso cref="Regex.pattern"/>
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
            _target = Verifier.IsNotNull(Verifier.VerifyArgument(target, nameof(target))).Value;
        }
        /// <summary>
        /// The base constructor of the <see cref="AbstractPathExpression"/> that is required by the implementing classes. 
        /// </summary>
        /// <param name="pattern">
        /// A valid regular expression pattern string.
        /// </param>
        /// <param name="regexFactory">
        /// A function that is responsible for creating the underlying regular expression which will be used by the 
        /// current <see cref="AbstractPathExpression"/> implementation.
        /// </param>
        protected AbstractPathExpression(string pattern, Func<string, RegexOptions, Regex> regexFactory) 
            : this(new RegularExpression(regexFactory(pattern, RegExOptions))) { }

        /// <inheritdoc />
        public bool IsMatch(string input) => _target.IsMatch(input);
        /// <inheritdoc />
        public bool IsMatch(string input, int startIndex) => _target.IsMatch(input, startIndex);

        /// <inheritdoc />
        public Match[] Match(string input) => _target.Match(input);
        /// <inheritdoc />
        public Match[] Match(string input, int startIndex) => _target.Match(input, startIndex);

        /// <inheritdoc />
        public string[] Split(string input) => _target.Split(input);
        /// <inheritdoc />
        public string[] Split(string input, int count) => _target.Split(input, count);
        /// <inheritdoc />
        public string[] Split(string input, int count, int startIndex) => _target.Split(input, count, startIndex);

        /// <summary>
        /// Returns the <see cref="Pattern"/> property's value.
        /// </summary>
        /// <returns>
        /// The <see cref="Pattern"/> property's value.
        /// </returns>
        public override string ToString() => Pattern;

        /// <summary>
        /// Gets the globbing pattern used by the current <see cref="IPathExpression"/> implementation.
        /// </summary>
        public abstract string Pattern { get; }
    }
}
#endif