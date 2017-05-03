using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="DateTime">time instant</see> to a valid <see cref="DateTime"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    public sealed class DateTimeParser : AbstractStrictParser<DateTime>
    {
        protected override DateTime DoParse(string value, IFormatProvider formatProvider)
        {
            return (formatProvider != null) ? DateTime.Parse(value, formatProvider) : DateTime.Parse(value);
        }

        protected override DateTime DoParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return DateTime.ParseExact(value, format, formatProvider);
        }
    }
}