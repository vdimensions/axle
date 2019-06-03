using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// An interface for a parser, that is, an object which is capable of converting a <see cref="string">string representation</see>
    /// of a given type to an instance of that type.
    /// </summary>
    /// <remarks>
    /// This interface is not intended for direct implementation. Its purpose is to allow a <see cref="IParser{T}">generic parser</see> instance
    /// to be used in code where the generic type parameter cannot be inferred or supplied. Therefore, it is assumed that implementations of this
    /// interface also implement the <see cref="IParser{T}">generic parser</see> interface.
    /// </remarks>
    /// <seealso cref="IParser{T}"/>
    public interface IParser
    {
        /// <summary>
        /// Parses a string to the specified type.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <returns>
        /// An instance created by the <see cref="IParser{T}">generic parser</see> implementation behind this interface.
        /// See remarks in the <see cref="IParser"/> interface for more info.
        /// </returns>
        /// <seealso cref="IParser{T}"/>
        object Parse(string value, IFormatProvider formatProvider);
        /// <summary>
        /// Parses a string to the specified type.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>
        /// An instance created by the <see cref="IParser{T}">generic parser</see> implementation behind this interface.
        /// See remarks in the <see cref="IParser"/> interface for more info.
        /// </returns>
        /// <seealso cref="IParser{T}"/>
        object Parse(string value);

        /// <summary>
        /// Converts the specified string representation of a logical value to its <see cref="TargetType"/> equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to convert.
        /// </param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed value created by the <see cref="IParser{T}">generic parser</see> implementation behind this interface,
        /// (see remarks in the <see cref="IParser"/> interface for more info) that is the equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or <c>null</c> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        /// <seealso cref="IParser{T}"/>
        bool TryParse(string value, IFormatProvider formatProvider, out object result);
        /// <summary>
        /// Converts the specified string representation of a logical value to its <see cref="TargetType"/> equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to convert.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains the parsed value created by the <see cref="IParser{T}">generic parser</see> implementation behind this interface,
        /// (see remarks in the <see cref="IParser"/> interface for more info) that is the equivalent to the string passed in <paramref name="value" />,
        /// if the conversion succeeded, or <c>null</c> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        /// <seealso cref="IParser{T}"/>
        bool TryParse(string value, out object result);

        /// <summary>
        /// The result type of the parsing.
        /// </summary>
        Type TargetType { get; }
    }

    /// <summary>
    /// An interface for a parser, that is, an object which is capable of converting a <see cref="string">string representation</see>
    /// of a given type to an instance of that type.
    /// </summary>
    /// <typeparam name="T">
    /// The result type of the parsing.
    /// </typeparam>
    public interface IParser<T> : IParser, IConverter<string, T>
    {
        /// <summary>
        /// Parses a string to the specified type.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        new T Parse(string value, IFormatProvider formatProvider);
        /// <summary>
        /// Parses a string to the specified type.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        new T Parse(string value);

        /// <summary>
        /// Converts the specified string representation of a logical value to its <typeparamref name="T"/> equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to convert.
        /// </param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <param name="output">
        /// When this method returns, contains the <typeparamref name="T"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <typeparamref name="T"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        bool TryParse(string value, IFormatProvider formatProvider, out T output);
        /// <summary>
        /// Converts the specified string representation of a logical value to its <typeparamref name="T"/> equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to convert.
        /// </param>
        /// <param name="output">
        /// When this method returns, contains the <typeparamref name="T"/> value equivalent to
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <typeparamref name="T"/> if the conversion has failed.
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        bool TryParse(string value, out T output);
    }
}
