using System;
using System.Globalization;
using System.IO;
using Axle.Resources;

namespace Axle.Data.DataSources.Resources
{
    /// <summary>
    /// A resource class to represent <see cref="SqlScriptSource">database sqlScriptSource</see> objects.
    /// </summary>
    /// <seealso cref="SqlScriptSource" />
    public sealed class SqlScriptSourceInfo : ResourceInfo
    {
        public const string MimeType = "application/sql";

        public const string FileExtension = ".sql";

        internal SqlScriptSourceInfo(SqlScriptSource sqlScriptSource, CultureInfo culture) : base(sqlScriptSource.Name, culture, MimeType)
        {
            Value = sqlScriptSource;
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            // TODO: think of a good way to represent a dictionary of sql scripts as a stream
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="SqlScriptSource"/> object represented by this <see cref="SqlScriptSourceInfo">resource</see> instance.
        /// </summary>
        public SqlScriptSource Value { get; }
    }
}