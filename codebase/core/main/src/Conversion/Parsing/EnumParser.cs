using System;


namespace Axle.Conversion.Parsing
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumParser<T> : AbstractParser<T>
    {
        /// <inheritdoc />
        protected override T DoParse(string value, IFormatProvider formatProvider)
        {
            try
            {
                // TODO: Create enum format provider specifying case option
                return (T) Enum.Parse(typeof(T), value);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                if (value.Trim().StartsWith("-"))
                {
                    var longParser = new Int64Parser();
                    if (longParser.TryParse(value, formatProvider, out var res))
                    {
                        return (T) Enum.ToObject(typeof(T), res);
                    }
                } 
                else
                {
                    var ulongParser = new UInt64Parser();
                    if (ulongParser.TryParse(value, formatProvider, out var res))
                    {
                        return (T) Enum.ToObject(typeof(T), res);
                    }
                }
                throw;
            }
        }
    }
}