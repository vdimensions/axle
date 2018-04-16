using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
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

        public bool IsMatch(string value) => _regex.IsMatch(value);
        public bool IsMatch(string value, int startIndex) => _regex.IsMatch(value, startIndex);

        public Match[] Match(string value) => _regex.Matches(value).Cast<Match>().ToArray();
        public Match[] Match(string value, int startIndex) => _regex.Matches(value, startIndex).Cast<Match>().ToArray();

        public string[] Split(string value) => _regex.Split(value);
        public string[] Split(string value, int count) => _regex.Split(value, count);
        public string[] Split(string value, int count, int startIndex) => _regex.Split(value, count, startIndex);

        /// <summary>
        /// Converts an instance of <see cref="RegularExpression"/> to the corresponding <see cref="Regex" /> equivalent
        /// </summary>
        /// <param name="expr">The <see cref="RegularExpression"/> instance to convert.</param>
        /// <returns>An instance of <see cref="Regex" /> equivalent the current <see cref="RegularExpression"/> instance</returns>
        /// <seealso cref="RegularExpression"/>
        /// <seealso cref="Regex"/>
        public static implicit operator Regex (RegularExpression expr) => expr._regex;
    }
}