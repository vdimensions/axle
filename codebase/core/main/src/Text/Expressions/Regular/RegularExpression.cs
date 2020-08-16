#if NETSTANDARD || NET20_OR_NEWER
using System.Diagnostics;
#if NETSTANDARD || NET35_OR_NEWER
using System.Linq;
#endif
using System.Text.RegularExpressions;
using Axle.Verification;


namespace Axle.Text.Expressions.Regular
{
    /// <summary>
    /// Represents a regular expression. Acts as a wrapper around the <see cref="Regex" /> class.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public class RegularExpression : IRegularExpression
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Regex _regex;

        /// <summary>
        /// Creates a new instance of the <see cref="RegularExpression"/> class using the provided <see cref="Regex"/> object.
        /// </summary>
        /// <param name="regex">
        /// Teh underlying <see cref="Regex"/> object.
        /// </param>
        public RegularExpression(Regex regex)
        {
            _regex = regex;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="RegularExpression"/> for the specified <paramref name="pattern"/>
        /// with <paramref name="options"/> that modify the pattern.
        /// </summary>
        /// <param name="pattern">
        /// The regular expression pattern to match.
        /// </param>
        /// <param name="options">
        /// </param>
        public RegularExpression(string pattern, RegexOptions options) : this(new Regex(pattern, options)) {}
        /// <summary>
        /// Creates a new instance of the <see cref="RegularExpression"/> for the specified <paramref name="pattern"/>.
        /// </summary>
        /// <param name="pattern">
        /// The regular expression pattern to match.
        /// </param>
        public RegularExpression(string pattern) : this(new Regex(pattern)) { }

        /// <inheritdoc />
        public bool IsMatch(string input) => _regex.IsMatch(input);
        /// <inheritdoc />
        public bool IsMatch(string input, int startIndex) => _regex.IsMatch(input, startIndex);

        /// <inheritdoc />
        #if NETSTANDARD || NET35_OR_NEWER
        public Match[] Match(string input) => _regex.Matches(input).Cast<Match>().ToArray();
        #else
        public Match[] Match(string input)
        {
            var result = _regex.Matches(input);
            var array = new Match[result.Count];
            result.CopyTo(array, 0);
            return array;
        }
        #endif
        /// <inheritdoc />
        #if NETSTANDARD || NET35_OR_NEWER
        public Match[] Match(string input, int startIndex) => _regex.Matches(input, startIndex).Cast<Match>().ToArray();
        #else
        public Match[] Match(string input, int startIndex)
        {
            var result = _regex.Matches(input, startIndex);
            var array = new Match[result.Count];
            result.CopyTo(array, 0);
            return array;
        }
        #endif

        /// <inheritdoc />
        public string Replace(string input, MatchEvaluator matchEvaluator)
        {
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(input, nameof(input)));
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(matchEvaluator, nameof(matchEvaluator)));
            return _regex.Replace(input, matchEvaluator);
        }
        /// <inheritdoc />
        public string Replace(string input, MatchEvaluator matchEvaluator, int count)
        {
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(input, nameof(input)));
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(matchEvaluator, nameof(matchEvaluator)));
            return _regex.Replace(input, matchEvaluator, count);
        }
        /// <inheritdoc />
        public string Replace(string input, MatchEvaluator matchEvaluator, int count, int startIndex)
        {
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(input, nameof(input)));
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(matchEvaluator, nameof(matchEvaluator)));
            ComparableVerifier.IsLessThan(Verifier.VerifyArgument(startIndex, nameof(startIndex)), input.Length);
            return _regex.Replace(input, matchEvaluator, count, startIndex);
        }
        /// <inheritdoc />
        public string Replace(string input, string replacement)
        {
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(input, nameof(input)));
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(replacement, nameof(replacement)));
            return _regex.Replace(input, replacement);
        }
        /// <inheritdoc />
        public string Replace(string input, string replacement, int count)
        {
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(input, nameof(input)));
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(replacement, nameof(replacement)));
            return _regex.Replace(input, replacement, count);
        }
        /// <inheritdoc />
        public string Replace(string input, string replacement, int count, int startIndex)
        {
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(input, nameof(input)));
            Verifier.IsNotNull(argument: Verifier.VerifyArgument(replacement, nameof(replacement)));
            ComparableVerifier.IsLessThan(Verifier.VerifyArgument(startIndex, nameof(startIndex)), input.Length);
            return _regex.Replace(input, replacement, count, startIndex);
        }

        /// <inheritdoc />
        public string[] Split(string input) => _regex.Split(input);
        /// <inheritdoc />
        public string[] Split(string input, int count) => _regex.Split(input, count);
        /// <inheritdoc />
        public string[] Split(string input, int count, int startIndex) => _regex.Split(input, count, startIndex);

        /// <summary>
        /// Converts an instance of <see cref="RegularExpression"/> to the corresponding <see cref="Regex" /> equivalent
        /// </summary>
        /// <param name="expr">The <see cref="RegularExpression"/> instance to convert.</param>
        /// <returns>An instance of <see cref="Regex" /> equivalent the current <see cref="RegularExpression"/> instance</returns>
        /// <seealso cref="RegularExpression"/>
        /// <seealso cref="Regex"/>
        public static implicit operator Regex (RegularExpression expr) => expr._regex;

        /// <summary>
        /// Converts an instance of <see cref="Regex" /> to the corresponding <see cref="RegularExpression"/> equivalent.
        /// </summary>
        /// <param name="expr">The <see cref="Regex"/> instance to convert.</param>
        /// <returns>An instance of <see cref="RegularExpression" /> equivalent the current <see cref="Regex"/> instance</returns>
        /// <seealso cref="RegularExpression"/>
        /// <seealso cref="Regex"/>
        public static explicit operator RegularExpression(Regex expr) => new RegularExpression(expr);
    }
}
#endif