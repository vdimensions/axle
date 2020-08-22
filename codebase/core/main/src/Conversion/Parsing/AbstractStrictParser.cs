using System;
using Axle.Text;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// An abstract class to serve as a base for implementing the <see cref="IStrictParser{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The result type of the parsing.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractStrictParser<T> : AbstractParser<T>, IStrictParser<T>
    {
        object IStrictParser.ParseExact(CharSequence value, string format, IFormatProvider formatProvider) => ParseExact(value, format, formatProvider);
        object IStrictParser.ParseExact(char[] value, string format, IFormatProvider formatProvider) => ParseExact(value, format, formatProvider);
        object IStrictParser.ParseExact(string value, string format, IFormatProvider formatProvider) => ParseExact(value, format, formatProvider);

        bool IStrictParser.TryParseExact(CharSequence value, string format, IFormatProvider formatProvider, out object result)
        {
            if (TryParseExact(value, format, formatProvider, out var res))
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }
        bool IStrictParser.TryParseExact(char[] value, string format, IFormatProvider formatProvider, out object result)
        {
            if (TryParseExact(value, format, formatProvider, out var res))
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }
        bool IStrictParser.TryParseExact(string value, string format, IFormatProvider formatProvider, out object result)
        {
            if (TryParseExact(value, format, formatProvider, out var res))
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }

        /// <inheritdoc />
        public T ParseExact(CharSequence value, string format, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }
            if (format.Length == 0)
            {
                throw new ArgumentException("Format must not be an empty string", nameof(format));
            }

            if (!ValidateExact(value, format, formatProvider))
            {
                throw new ParseException(
                    string.Format("Unable to parse a character array to the desired type `{0}`.", typeof(T)));
            }

            try
            {
                return DoParseExact(value, format, formatProvider);
            }
            catch (Exception ex)
            {
                throw new ParseException(
                    string.Format(
                        "An error occurred while parsing a character array to the desired type `{0}`. See the inner exception for more details.", 
                        typeof(T)), 
                    ex);
            }
        }
        /// <inheritdoc />
        public T ParseExact(char[] value, string format, IFormatProvider formatProvider) 
            => ParseExact((CharSequence) value, format, formatProvider);
        /// <inheritdoc />
        public T ParseExact(string value, string format, IFormatProvider formatProvider) 
            => ParseExact((CharSequence) value, format, formatProvider);

        /// <inheritdoc />
        public virtual bool TryParseExact(CharSequence value, string format, IFormatProvider formatProvider, out T output)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrEmpty(format) || !ValidateExact(value, format, formatProvider))
            {
                output = default(T);
                return false;
            }
            var result = true;
            try
            {
                output = DoParseExact(value, format, formatProvider);
            }
            catch
            {
                output = default(T);
                result = false;
            }
            return result;
        }
        /// <inheritdoc />
        public bool TryParseExact(char[] value, string format, IFormatProvider formatProvider, out T output)
            => TryParseExact((CharSequence) value, format, formatProvider, out output);
        /// <inheritdoc />
        public bool TryParseExact(string value, string format, IFormatProvider formatProvider, out T output)
            => TryParseExact((CharSequence) value, format, formatProvider, out output);

        /// <summary>
        /// This method is called within the <see cref="TryParseExact(string,string,System.IFormatProvider,out T)"/> to
        /// further validate the provided value and format, before the parsing is attempted.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string"/> containing the value to convert. 
        /// </param>
        /// <param name="format">
        /// A format string specifying the format of the value to parse. 
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific 
        /// format recognition. 
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c>, if the validation logic determines that the value is a valid representation 
        /// of <typeparamref name="T"/>;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public virtual bool ValidateExact(CharSequence value, string format, IFormatProvider formatProvider) => true;

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
        /// <param name="format">
        /// A format string specifying the format of the value to parse. 
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific 
        /// format recognition. 
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        protected abstract T DoParseExact(CharSequence value, string format, IFormatProvider formatProvider);
        
        T IConverter<CharSequence, T>.Convert(CharSequence source) => Parse(source);
        bool IConverter<CharSequence, T>.TryConvert(CharSequence source, out T target) => TryParse(source, out target);

        T IConverter<char[], T>.Convert(char[] source) => Parse(source);
        bool IConverter<char[], T>.TryConvert(char[] source, out T target) => TryParse(source, out target);
        
        T IConverter<string, T>.Convert(string source) => Parse(source);
        bool IConverter<string, T>.TryConvert(string source, out T target) => TryParse(source, out target);
    }
}
