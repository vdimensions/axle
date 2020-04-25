using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Axle.Resources.Text.Documents;
using YamlDotNet.Core;
using YamlDotNet.Serialization;


namespace Axle.Resources.Yaml
{
    /// <summary>
    /// A class representing a YAML file as a resource.
    /// </summary>
    public sealed class YamlResourceInfo : TextDocumentResourceInfo
    {
        /// <summary>
        /// Gets the content (MIME) type of a java properties file.
        /// </summary>
        public const string MimeType = "application/x-yaml";

        /// <summary>
        /// Gets the file extension for a Java properties file.
        /// </summary>
        public const string FileExtension = ".yml";

        internal YamlResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data) 
            : base(name, culture, MimeType, data)
        {
        }
        internal YamlResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data, ResourceInfo originalResource) 
            : base(name, culture, MimeType, data, originalResource)
        {
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
                return base.Open();
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            var result = new MemoryStream();
            var writer = new StreamWriter(result, Encoding.UTF8);
            new Serializer().Serialize(writer, Data);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <inheritdoc />
        public override bool TryResolve(Type targetType, out object result)
        {
            try
            {
                var deserializer = new Deserializer();
                using (var stream = Open())
                using (var reader = new StreamReader(stream))
                {
                    result = deserializer.Deserialize(new Parser(reader), targetType);
                    return true;
                }
            }
            catch
            {
                if (base.TryResolve(targetType, out result))
                {
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
