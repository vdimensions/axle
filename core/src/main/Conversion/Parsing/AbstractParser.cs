using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// An abstract class to be used as a base class for any
    /// custom type parser.
    /// Supports input validation prior parsing.
    /// </summary>
    /// <typeparam name="T">
    /// The type a string is to be parsed to.
    /// </typeparam>
    #if !netstandard
    [Serializable]
    #endif
    public abstract class AbstractParser<T> : IParser<T>
    {
        object IParser.Parse(string value, IFormatProvider formatProvider) { return Parse(value, formatProvider); }
        object IParser.Parse(string value) { return Parse(value); }

        bool IParser.TryParse(string value, IFormatProvider formatProvider, out object result)
        {
            T genericResult;
            if (TryParse(value, formatProvider, out genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(string value, out object result)
        {
            T genericResult;
            if (TryParse(value, out genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }

        /// <summary>
        /// Parses a string to the specified type.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the string cannot be recognized
        /// as a valid provider for and instance of <typeparamref name="T"/>.</exception>
        public T Parse(string value) { return Parse(value, null); }
        public T Parse(string value, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Validate(value, formatProvider))
            {
                throw new ParseException(value, typeof(T));
            }

            try
            {
                return DoParse(value, formatProvider);
            }
            catch (Exception ex)
            {
                throw new ParseException(value, typeof(T), ex);
            }
        }

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
        public virtual bool TryParse(string value, out T output) { return TryParse(value, null, out output); }
        public virtual bool TryParse(string value, IFormatProvider formatProvider, out T output)
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
            catch
            {
                output = default(T);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Attempts to create an instance of the specified type,
        /// but does not perform any validation of the input string.
        /// <remarks>
        /// This method is intended to be used after a string validation was performed.
        /// </remarks> 
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="formatProvider"></param>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        protected abstract T DoParse(string value, IFormatProvider formatProvider);

        /// <summary>
        /// Performs validation of the input string to ensure
        /// that safe parsing is possible.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <returns>true if the value can be parsed to the specified type; false otherwise</returns>
        public virtual bool Validate(string value, IFormatProvider formatProvider) { return true; }

        T IConverter<string, T>.Convert(string source) { return Parse(source); }
        bool IConverter<string, T>.TryConvert(string source, out T target) { return TryParse(source, out target); }

        public Type TargetType { get { return typeof(T); } }
    }
}