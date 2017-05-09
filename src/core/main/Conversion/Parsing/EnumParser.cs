using System;


namespace Axle.Conversion.Parsing
{
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class EnumParser<T> : AbstractParser<T>
    {
        protected override T DoParse(string value, IFormatProvider formatProvider)
        {
            try
            {
                // TODO: Create enum format provider specifying case option
                return (T) Enum.Parse(typeof(T), value);
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                if (value.Trim().StartsWith("-"))
                {
                    long res;
                    var longParser = new Int64Parser();
                    if (longParser.TryParse(value, formatProvider, out res))
                    {
                        return (T) Enum.ToObject(typeof(T), res);
                    }
                } 
                else
                {
                    ulong res;
                    var ulongParser = new UInt64Parser();
                    if (ulongParser.TryParse(value, formatProvider, out res))
                    {
                        return (T) Enum.ToObject(typeof(T), res);
                    }
                }
                throw;
            }
        }
    }
}