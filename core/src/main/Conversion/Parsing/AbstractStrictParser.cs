using System;


namespace Axle.Conversion.Parsing
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public abstract class AbstractStrictParser<T> : AbstractParser<T>, IStrictParser<T>
    {
        object IStrictParser.ParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return ParseExact(value, format, formatProvider);
        }

        bool IStrictParser.TryParseExact(string value, string format, IFormatProvider formatProvider, out object result)
        {
            if (this.TryParseExact(value, format, formatProvider, out var res))
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }

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

        public virtual bool ValidateExact(string value, string format, IFormatProvider formatProvider)
        {
            return true;
        }

        protected abstract T DoParseExact(string value, string format, IFormatProvider formatProvider);

        T IConverter<string, T>.Convert(string source) { return Parse(source); }
        bool IConverter<string, T>.TryConvert(string source, out T target) { return TryParse(source, out target); }
    }
}