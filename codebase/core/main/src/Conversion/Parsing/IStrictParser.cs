using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// An interface for a strict parser; that is, a parser which may use additional format specifications for a raw string value
    /// in order to parse it into an object instance.
    /// </summary>
    /// <seealso cref="IParser" />
    public interface IStrictParser : IParser
    {
        /// <summary>
        /// Parses a <see cref="string">string</see> <paramref name="value"/> to the specified type while conforming
        /// to a specific <paramref name="format"/>.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>
        /// An object that is the result of parsing <paramref name="value"/>.
        /// </returns>
        /// <param name="format">
        /// A format string specifying the format of the <paramref name="value"/> to parse.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide
        /// culture-specific format recognition.
        /// </param>
        /// <returns>
        /// An object that is the result of parsing <paramref name="value"/>.
        /// </returns>
        object ParseExact(string value, string format, IFormatProvider formatProvider);

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its object equivalent, while conforming to a specific <paramref name="format"/>.
        /// A <see cref="bool">boolean</see> return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string">string</see> containing the value to convert.
        /// </param>
        /// <param name="format">
        /// A format string specifying the format of the <paramref name="value"/> to parse.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide
        /// culture-specific format recognition.
        /// </param>
        /// <param name="result">
        /// When this method returns, <paramref name="result"/> contains the parsed object value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or <c>null</c> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c>null</c> or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if value was converted successfully; otherwise, <c>false</c>.
        /// </returns>
        bool TryParseExact(string value, string format, IFormatProvider formatProvider, out object result);
    }

    /// <summary>
    /// A generic version of the <see cref="IStrictParser"/> interface.
    /// </summary>
    /// <seealso cref="IParser{T} "/>
    public interface IStrictParser<T> : IStrictParser, IParser<T>
    {
        /// <summary>
        /// Parses a <see cref="string">string</see> <paramref name="value"/> to the specified type while conforming
        /// to a specific <paramref name="format"/>.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>
        /// An instance of <typeparamref name="T" /> that is the result of parsing <paramref name="value"/>.
        /// </returns>
        /// <param name="format">
        /// A format string specifying the format of the <paramref name="value"/> to parse.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide
        /// culture-specific format recognition.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" /> that is the result of parsing <paramref name="value"/>.
        /// </returns>
        new T ParseExact(string value, string format, IFormatProvider formatProvider);

        /// <summary>
        /// Converts the specified <see cref="string">string</see> representation of a logical <paramref name="value"/>
        /// to its <typeparamref name="T"/> equivalent, while conforming to a specific <paramref name="format"/>.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string">string</see> containing the value to convert.
        /// </param>
        /// <param name="format">
        /// A format string specifying the format of the <paramref name="value"/> to parse.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide
        /// culture-specific format recognition.
        /// </param>
        /// <param name="result">
        /// When this method returns, <paramref name="result"/> contains the <typeparamref name="T"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <typeparamref name="T"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is <c>null</c> or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if value was converted successfully; otherwise, <c>false</c>.
        /// </returns>
        bool TryParseExact(string value, string format, IFormatProvider formatProvider, out T result);
    }
}
