#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Net.Mime;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of <see cref="ContentType">content type</see> 
    /// to a valid <see cref="ContentType"/> instance.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ContentTypeParser : AbstractParser<ContentType>
    {
        /// <inheritdoc />
        protected override ContentType DoParse(string value, IFormatProvider formatProvider)
        {
            return new ContentType(value);
        }
    }
}
#endif