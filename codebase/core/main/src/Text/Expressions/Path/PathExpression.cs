#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Axle.Extensions.String;


namespace Axle.Text.Expressions.Path
{
    /// <summary>
    /// A class representing a filesystem globbing expression.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class PathExpression : AbstractPathExpression
    {
        private const string AntSingleAsteriskWinRegex = @"(?:(?<=[\\]{0,1})(?:[^\\]+)(?=[\\]{0,1}))";
        private const string AntSingleAsteriskUnixRegex = @"(?<=[/]{0,1})(?:[^/]+)(?=[/]{0,1})";
        private const string AntDoubleAsteriskRegex = @"(?:.{0,})";

        private static Regex CreateRegex(string pattern, RegexOptions options)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var res = EscapeRegex(pattern);
            if (!StringExtensions.Contains(res, "/", StringComparison.Ordinal))
            {
                // assume windows path

                // windows path is not case-sensitive
                options |= RegexOptions.IgnoreCase;

                //var unixRes = res
                //    .Replace("\\\\", "/")
                //    .Replace("**", AntDoubleAsteriskRegex)
                //    .Replace("*", AntSingleAsteriskUnixRegex);
                var winRes = res
                    .Replace("**", AntDoubleAsteriskRegex)
                    .Replace("*", AntSingleAsteriskWinRegex);
                res = winRes;//string.Format("(?<unixpath>{0})|(?<windowspath>{1})", unixRes, winRes);
            }
            else
            {
                // assume unix path

                var unixRes = res
                    .Replace("**", AntDoubleAsteriskRegex)
                    .Replace("*", AntSingleAsteriskUnixRegex);
                //var winRes = res
                //    .Replace("/", "\\\\")
                //    .Replace("**", AntDoubleAsteriskRegex)
                //    .Replace("*", AntSingleAsteriskWinRegex);
                res = unixRes;// string.Format("(?<unixpath>{0})|(?<windowspath>{1})", unixRes, winRes);
            }
            return new Regex(res, options);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _pattern;

        /// <summary>
        /// Creates a new path expression using the specified <paramref name="pattern"/>.
        /// </summary>
        /// <param name="pattern">
        /// The globbing pattern to match.
        /// </param>
        public PathExpression(string pattern) : base(pattern, CreateRegex)
        {
            _pattern = pattern;
        }

        /// <summary>
        /// Gets the pattern used by the current <see cref="PathExpression"/>.
        /// </summary>
        public override string Pattern => _pattern;
    }
}
#endif