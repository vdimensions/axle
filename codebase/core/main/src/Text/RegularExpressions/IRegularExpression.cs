#if NETSTANDARD || NET20_OR_NEWER
using System;

namespace Axle.Text.RegularExpressions
{
    [Obsolete("Use `Axle.Text.Expressions.Regular.IRegularExpression` instead")]
    public interface IRegularExpression : Axle.Text.Expressions.Regular.IRegularExpression { }
}
#endif