using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

using YamlDotNet.Serialization;


namespace Axle.Resources.Yaml
{
    /// <summary>
    /// A class representing a YAML file as a resource.
    /// </summary>
    public sealed class YamlResourceInfo : ResourceInfo
    {
        private readonly IEnumerable<IDictionary<string, string>> _data;
        private readonly ResourceInfo _originalResource;

        /// <summary>
        /// Gets the content (MIME) type of a java properties file.
        /// </summary>
        public const string MimeType = "application/x-yaml";

        /// <summary>
        /// Gets the file extension for a Java properties file.
        /// </summary>
        public const string FileExtension = ".yml";

        internal YamlResourceInfo(string name, CultureInfo culture, IEnumerable<IDictionary<string, string>> data) : base(name, culture, MimeType)
        {
            _data = data;
        }
        internal YamlResourceInfo(string name, CultureInfo culture, IEnumerable<IDictionary<string, string>> data, ResourceInfo originalResource) 
            : this(name, culture, data)
        {
            _originalResource = originalResource;
        }

        /// <summary>
        /// Opens a <see cref="Stream">stream</see> object for reading the contents of the YAML file.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream">stream</see> object for reading the contents of the YAML file.
        /// </returns>
        public override Stream Open()
        {
            // ReSharper disable EmptyGeneralCatchClause
            try
            {
                var stream = _originalResource?.Open();
                if (stream != null)
                {
                    return stream;
                }
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            var result = new MemoryStream();
            var writer = new StreamWriter(result, Encoding.UTF8);
            new Serializer().Serialize(writer, _data);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <summary>
        /// Gets a <see cref="IDictionary{TKey,TValue}"/> representing the contents of the YAML file.
        /// </summary>
        //TODO: make the data read-only
        public IEnumerable<IDictionary<string, string>> Data => _data;
    }
}
