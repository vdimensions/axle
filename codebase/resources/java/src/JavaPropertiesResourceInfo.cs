using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Kajabity.Tools.Java;


namespace Axle.Resources.Java
{
    /// <summary>
    /// A class representing a Java properties file as a resource.
    /// </summary>
    public class JavaPropertiesResourceInfo : ResourceInfo
    {
        /// <summary>
        /// Gets the content (MIME) type of the java properties file.
        /// </summary>
        public const string MimeType = "text/x-java-properties";

        /// <summary>
        /// Gets the file extension for a Java properties file.
        /// </summary>
        public const string FileExtension = ".properties";

        private readonly Dictionary<string, string> _data;

        internal JavaPropertiesResourceInfo(string name, CultureInfo culture, Dictionary<string, string> data) : base(name, culture, MimeType)
        {
            _data = data;
        }

        /// <summary>
        /// Opens a <see cref="Stream">stream</see> object for reading the contents of the properties file.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream">stream</see> object for reading the contents of the properties file.
        /// </returns>
        public override Stream Open()
        {
            var result = new MemoryStream();
            new JavaPropertyWriter(_data).Write(result, null);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <summary>
        /// Gets a <see cref="IDictionary{TKey,TValue}"/> representing the contents of the properties file.
        /// </summary>
        //TODO: make the data read-only
        public IDictionary<string, string> Data => _data;
    }
}