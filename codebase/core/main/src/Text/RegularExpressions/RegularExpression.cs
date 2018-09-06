#if NETSTANDARD || NET35_OR_NEWER
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public class RegularExpression : IRegularExpression
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Regex _regex;

        public RegularExpression(Regex regex)
        {
            _regex = regex;
        }
        public RegularExpression(string patterm, RegexOptions options) : this(new Regex(patterm, options)) {}
        public RegularExpression(string patterm) : this(new Regex(patterm)) { }

        /// <inheritdoc />
        public bool IsMatch(string input) => _regex.IsMatch(input);
        /// <inheritdoc />
        public bool IsMatch(string input, int startIndex) => _regex.IsMatch(input, startIndex);

        /// <inheritdoc />
        public Match[] Match(string input) => _regex.Matches(input).Cast<Match>().ToArray();
        /// <inheritdoc />
        public Match[] Match(string input, int startIndex) => _regex.Matches(input, startIndex).Cast<Match>().ToArray();

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