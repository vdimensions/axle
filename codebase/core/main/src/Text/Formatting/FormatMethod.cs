#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Text.Formatting
{
    public delegate string FormatMethod(string format, object arg, IFormatProvider formatProvider);
    public delegate string FormatMethod<T>(string format, T arg, IFormatProvider formatProvider);
    public delegate string FormatMethod<T, TFP>(string format, T arg, TFP formatProvider);
}
#endif