using System;
using System.Globalization;
using System.IO;

using Axle.Resources;


namespace Axle.Data.Resources
{
    /// <summary>
    /// A resource class to represent <see cref="Query">database query</see> objects.
    /// </summary>
    /// <seealso cref="Query" />
    public sealed class QueryResourceInfo : ResourceInfo
    {
        public const string MimeType = "application/sql";

        public const string FileExtension = ".sql";

        internal QueryResourceInfo(Query query, CultureInfo culture) : base(query.Name, culture, MimeType)
        {
            Value = query;
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            // TODO:
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="Query"/> object represented by this <see cref="QueryResourceInfo">resource</see> instance.
        /// </summary>
        public Query Value { get; }
    }
}