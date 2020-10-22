using System;

namespace Axle.Text.Parsing
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
        protected override DateTime DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            var dateOffset = new DateTimeOffsetParser().ParseExact(
                value.ToString(),
                "yyyy-MM-dd'T'HH:mm:ss.FFFK",
                formatProvider);
            var dt = dateOffset.DateTime.AddMilliseconds(dateOffset.Offset.TotalMilliseconds);
            return dt;
        }
    }
}
