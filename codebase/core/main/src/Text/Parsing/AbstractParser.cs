using System;
using Axle.Conversion;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// An abstract class to aid the implementation of a custom parser.
    /// Supports optional input validation prior parsing.
    /// </summary>
    /// <typeparam name="T">
    /// The result type of the parsing.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractParser<T> : IParser<T>
    {
        object IParser.Parse(CharSequence value, IFormatProvider formatProvider) => Parse(value, formatProvider);
        object IParser.Parse(CharSequence value) => Parse(value);
        object IParser.Parse(char[] value, IFormatProvider formatProvider) => Parse(value, formatProvider);
        object IParser.Parse(char[] value) => Parse(value);
        object IParser.Parse(string value, IFormatProvider formatProvider) => Parse(value, formatProvider);
        object IParser.Parse(string value) => Parse(value);

        bool IParser.TryParse(CharSequence value, IFormatProvider formatProvider, out object result)
        {
            if (TryParse(value, formatProvider, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(CharSequence value, out object result)
        {
            if (TryParse(value, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(char[] value, IFormatProvider formatProvider, out object result)
        {
            if (TryParse(value, formatProvider, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(char[] value, out object result)
        {
            if (TryParse(value, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(string value, IFormatProvider formatProvider, out object result)
        {
            if (TryParse(value, formatProvider, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(string value, out object result)
        {
            if (TryParse(value, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        
        /// <summary>
        /// Parses a <see cref="char">character</see> sequence to the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">
        /// The character sequence value to be parsed.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the string cannot be recognized as a valid representation of <typeparamref name="T"/> instance.
        /// </exception>
        public T Parse(CharSequence value) => Parse(value, null);
        /// <inheritdoc />
        public T Parse(CharSequence value, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Validate(value, formatProvider))
            {
                throw new ParseException(
                    string.Format(value.ToString(), typeof(T)));
            }

            try
            {
                return DoParse(value, formatProvider);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ParseException( value.ToString(), typeof(T), ex);
            }
        }
        /// <summary>
        /// Parses a <see cref="char">character</see> array to the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="char">character</see> array value to be parsed.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the string cannot be recognized as a valid representation of <typeparamref name="T"/> instance.
        /// </exception>
        public T Parse(char[] value) => Parse(value, null);
        /// <inheritdoc />
        public T Parse(char[] value, IFormatProvider formatProvider) 
            => Parse((CharSequence) value, formatProvider);

        /// <summary>
        /// Parses a <see cref="string"/> to the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">
        /// The string value to be parsed.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the string cannot be recognized as a valid representation of <typeparamref name="T"/> instance.
        /// </exception>
        public T Parse(string value) => Parse(value, null);
        /// <inheritdoc />
        public T Parse(string value, IFormatProvider formatProvider) 
            => Parse((CharSequence) value, formatProvider);

        /// <summary>
        /// Converts the specified <see cref="char">character</see> sequence representation of a logical value to its
        /// <typeparamref name="T"/> equivalent. 
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A character sequence containing the value to convert.
        /// </param>
        /// <param name="output">
        /// When this method returns, contains the <typeparamref name="T"/> value equivalent to 
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <typeparamref name="T"/> if the conversion failed. 
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if value was converted successfully; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool TryParse(CharSequence value, out T output) => TryParse(value, null, out output);
        /// <inheritdoc />
        public virtual bool TryParse(CharSequence value, IFormatProvider formatProvider, out T output)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Validate(value, formatProvider))
            {
                output = default(T);
                return false;
            }
            var result = true;
            try
            {
                output = DoParse(value, formatProvider);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch
            {
                output = default(T);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Converts the specified <see cref="char">character</see> array representation of a logical value to its
        /// <typeparamref name="T"/> equivalent. 
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A character array containing the value to convert.
        /// </param>
        /// <param name="output">
        /// When this method returns, contains the <typeparamref name="T"/> value equivalent to 
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <typeparamref name="T"/> if the conversion failed. 
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        public bool TryParse(char[] value, out T output) => TryParse((CharSequence) value, out output);
        /// <inheritdoc />
        public bool TryParse(char[] value, IFormatProvider formatProvider, out T output) 
            => TryParse((CharSequence) value, formatProvider, out output);

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
        /// value for <typeparamref name="T"/> if the conversion failed. 
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        public bool TryParse(string value, out T output) 
            => TryParse((CharSequence) value, out output);
        /// <inheritdoc />
        public bool TryParse(string value, IFormatProvider formatProvider, out T output) 
            => TryParse((CharSequence) value, formatProvider, out output);

        /// <summary>
        /// Attempts to create an instance of the specified type, but does not perform any validation of the input
        /// <see cref="char">character</see> sequence.
        /// <remarks>
        /// This method is intended to be used after a string validation was performed.
        /// </remarks> 
        /// </summary>
        /// <param name="value">
        /// The <see cref="char">character</see> sequence value to be parsed.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific format recognition. 
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        protected abstract T DoParse(CharSequence value, IFormatProvider formatProvider);
        
        /// <summary>
        /// Performs validation of the input string to ensure
        /// that safe parsing is possible.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <returns>true if the value can be parsed to the specified type; false otherwise</returns>
        public virtual bool Validate(CharSequence value, IFormatProvider formatProvider) => true;
        
        T IConverter<CharSequence, T>.Convert(CharSequence source) => Parse(source);
        bool IConverter<CharSequence, T>.TryConvert(CharSequence source, out T target) => TryParse(source, out target);
        
        T IConverter<char[], T>.Convert(char[] source) => Parse(source);
        bool IConverter<char[], T>.TryConvert(char[] source, out T target) => TryParse(source, out target);

        T IConverter<string, T>.Convert(string source) => Parse(source);
        bool IConverter<string, T>.TryConvert(string source, out T target) => TryParse(source, out target);

        /// <inheritdoc />
        public Type TargetType => typeof(T);
    }
}
