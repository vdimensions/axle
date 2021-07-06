using System;
using Axle.Text.Parsing;
using Axle.Verification;

namespace Axle.Text.Formatting.Extensions
{
    /// <summary>
    /// A static class containing extension methods for the <see cref="IFormatProvider"/> interface.
    /// </summary>
    public static class FormatProviderExtensions
    {
        /// <summary>
        /// Replaces the format items in a specified <paramref name="format"/> string
        /// with the string representations of corresponding objects in a specified <paramref name="args"/> array.
        /// The <paramref name="formatProvider"/> parameter supplies culture-specific formatting information.
        /// </summary>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An array that contains zero or more objects to format.
        /// </param>
        /// <returns>
        /// A copy of the <paramref name="format"/> string in which the format items have been replaced
        /// by the string representation of the corresponding objects in the <paramref name="args"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="formatProvider"/>, <paramref name="format"/> or <paramref name="args"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para>
        /// <paramref name="format"/>is invalid.
        /// </para>
        /// -or-
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <seealso cref="string.Format(IFormatProvider,string,object[])"/>
        public static string Format(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IFormatProvider formatProvider, string format, params object[] args)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(formatProvider, nameof(formatProvider)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(format, nameof(format)));
            return string.Format(formatProvider, format, args);
        }

        /// <summary>
        /// Parses a <see cref="string">string</see> <paramref name="value"/> to the specified by the <typeparamref name="T"/> type. 
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parsing result.
        /// </typeparam>
        /// <param name="formatProvider">
        /// The <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific
        /// format recognition.
        /// </param>
        /// <param name="parser">
        /// A <see cref="IParser{T}"/> instance to perform the parsing.
        /// </param>
        /// <param name="value">
        /// The <see cref="char">character</see> sequence value to parse.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> which is the result of parsing the provided <paramref name="value"/>.
        /// </returns>
        public static T Parse<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IFormatProvider formatProvider, IParser<T> parser, CharSequence value)
        {
            return parser.Parse(
                CharSequenceVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(value, nameof(value))), 
                Verifier.IsNotNull(Verifier.VerifyArgument(formatProvider, nameof(formatProvider))).Value);
        }
        /// <summary>
        /// Parses a <see cref="string">string</see> <paramref name="value"/> to the specified by the <typeparamref name="T"/> type. 
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parsing result.
        /// </typeparam>
        /// <param name="formatProvider">
        /// The <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific
        /// format recognition.
        /// </param>
        /// <param name="parser">
        /// A <see cref="IParser{T}"/> instance to perform the parsing.
        /// </param>
        /// <param name="value">
        /// The <see cref="string">string</see> value to parse.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> which is the result of parsing the provided <paramref name="value"/>.
        /// </returns>
        public static T Parse<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IFormatProvider formatProvider, IParser<T> parser, string value)
        {
            return parser.Parse(
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(value, nameof(value))), 
                Verifier.IsNotNull(Verifier.VerifyArgument(formatProvider, nameof(formatProvider))).Value);
        }

        /// <summary>
        /// Parses a <see cref="string">string</see> <paramref name="value"/> to the specified by the <typeparamref name="T"/> type. 
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parsing result.
        /// </typeparam>
        /// <param name="formatProvider">
        /// The <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide
        /// culture-specific format recognition.
        /// </param>
        /// <param name="parser">
        /// A <see cref="IStrictParser{T}"/> instance to perform the parsing.
        /// </param>
        /// <param name="format">
        /// A format string specifying the format of the <paramref name="value"/> to parse.
        /// </param>
        /// <param name="value">
        /// The <see cref="CharSequence">character sequence</see> value to parse.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> which is the result of parsing the provided <paramref name="value"/>.
        /// </returns>
        public static T ParseExact<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IFormatProvider formatProvider, IStrictParser<T> parser, string format, CharSequence value)
        {
            return parser.ParseExact(
                CharSequenceVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(value, nameof(value))), 
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(format, nameof(format))), 
                Verifier.IsNotNull(Verifier.VerifyArgument(formatProvider, nameof(formatProvider))).Value);
        }
        /// <summary>
        /// Parses a <see cref="string">string</see> <paramref name="value"/> to the specified by the <typeparamref name="T"/> type. 
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parsing result.
        /// </typeparam>
        /// <param name="formatProvider">
        /// The <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide
        /// culture-specific format recognition.
        /// </param>
        /// <param name="parser">
        /// A <see cref="IStrictParser{T}"/> instance to perform the parsing.
        /// </param>
        /// <param name="format">
        /// A format string specifying the format of the <paramref name="value"/> to parse.
        /// </param>
        /// <param name="value">
        /// The <see cref="string">string</see> value to parse.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> which is the result of parsing the provided <paramref name="value"/>.
        /// </returns>
        public static T ParseExact<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IFormatProvider formatProvider, IStrictParser<T> parser, string format, string value)
        {
            return parser.ParseExact(
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(value, nameof(value))), 
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(format, nameof(format))), 
                Verifier.IsNotNull(Verifier.VerifyArgument(formatProvider, nameof(formatProvider))).Value);
        }
    }
}
