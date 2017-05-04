using System;


namespace Axle.Text.Formatting
{
    public interface ICustomFormatter<T> : ICustomFormatter
    {
        string Format(string formatString, T arg, IFormatProvider formatProvider);
    }

    public interface ICustomFormatter<T, TFP> : ICustomFormatter<T> where TFP: IFormatProvider
    {
        string Format(string formatString, T arg, TFP formatProvider);
    }
}