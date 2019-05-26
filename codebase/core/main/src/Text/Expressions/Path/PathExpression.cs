#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Axle.Extensions.String;


namespace Axle.Text.Expressions.Path
{
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

                // windows path is not case-sesnsitive
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

        public PathExpression(string pattern) : base(pattern, CreateRegex)
        {
            _pattern = pattern;
        }

        public override string Pattern => _pattern;
    }
}
#endif