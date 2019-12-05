using System;


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
        object IStrictParser.ParseExact(string value, string format, IFormatProvider formatProvider) => ParseExact(value, format, formatProvider);

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
        public T ParseExact(string value, string format, IFormatProvider formatProvider)
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
                throw new ParseException(value, format, typeof(T));
            }

            try
            {
                return DoParseExact(value, format, formatProvider);
            }
            catch (Exception ex)
            {
                throw new ParseException(value, format, typeof(T), ex);
            }
        }

        /// <inheritdoc />
        public virtual bool TryParseExact(string value, string format, IFormatProvider formatProvider, out T output)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            //if (format == null)
            //{
            //    throw new ArgumentNullException("forma");
            //}

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
        public virtual bool ValidateExact(string value, string format, IFormatProvider formatProvider)
        {
            return true;
        }

        /// <summary>
        /// Attempts to create an instance of the specified type, but does not perform any validation of the input 
        /// string.
        /// <remarks>
        /// This method is intended to be used after a string validation was performed.
        /// </remarks> 
        /// </summary>
        /// <param name="value">
        /// The string value to be parsed.
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
        protected abstract T DoParseExact(string value, string format, IFormatProvider formatProvider);

        T IConverter<string, T>.Convert(string source) => Parse(source);
        bool IConverter<string, T>.TryConvert(string source, out T target) => TryParse(source, out target);
    }
}
