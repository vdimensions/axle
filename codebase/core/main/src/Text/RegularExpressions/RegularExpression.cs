#if NETSTANDARD || NET35_OR_NEWER
using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    [System.Obsolete("Use `Axle.Text.Expressions.Regular.RegularExpression` instead")]
    public class RegularExpression : Axle.Text.Expressions.Regular.RegularExpression, IRegularExpression
    {
        public RegularExpression(Regex regex) : base(regex) { }
        public RegularExpression(string patterm, RegexOptions options) : base(patterm, options) { }
        public RegularExpression(string patterm) : base(patterm) { }
    }
}
#endif