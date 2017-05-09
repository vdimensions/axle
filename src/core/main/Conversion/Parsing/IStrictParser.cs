using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// An interface for a strict parser; that is, a parser which may use additional formatting prerequisites in order to parse
    /// a raw string value into an object instance. 
    /// </summary>
    /// <seealso cref="IParser" />
    public interface IStrictParser : IParser
    {
        object ParseExact(string value, string format, IFormatProvider formatProvider);

        bool TryParseExact(string value, string format, IFormatProvider formatProvider, out object result);
    }
    /// <summary>
    /// A generic version of the <see cref="IStrictParser"/> interface.
    /// </summary>
    /// <seealso cref="IParser{T} "/>
    public interface IStrictParser<T> : IStrictParser, IParser<T>
    {
        new T ParseExact(string value, string format, IFormatProvider formatProvider);

        bool TryParseExact(string value, string format, IFormatProvider formatProvider, out T result);
    }
}