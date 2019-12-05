using System;

namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse ISO 8601 <see cref="string">string</see> representations of a
    /// <see cref="DateTime">time instant</see> to a valid <see cref="DateTime"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DateTimeISOParser : AbstractParser<DateTime>
    {
        /// <inheritdoc />
        protected override DateTime DoParse(string value, IFormatProvider formatProvider)
        {
            var dtoParser = new DateTimeOffsetParser();
            var dateOffset = dtoParser.ParseExact(
                value,
                "yyyy-MM-dd'T'HH:mm:ss.FFFK",
                formatProvider);
            DateTime dt = dateOffset.DateTime.AddMilliseconds(dateOffset.Offset.TotalMilliseconds);
            return dt;
        }
    }
}
