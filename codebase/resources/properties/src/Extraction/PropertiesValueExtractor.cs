using System.Collections.Generic;
using System.Text;
using Axle.Resources.Extraction;
using Axle.Resources.Text.Documents;
using Axle.Text;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementation that can access the values defined in a Java properties file.
    /// </summary>
    internal sealed class PropertiesValueExtractor : AbstractResourceExtractor
    {
        private readonly Encoding _encoding;
        private readonly string _propertyFileName;
        private readonly string _keyPrefix;

        public PropertiesValueExtractor(Encoding encoding, string propertyFileName, string keyPrefix)
        {
            _encoding = encoding;
            _propertyFileName = propertyFileName;
            _keyPrefix = keyPrefix;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Attempts to locate a string value with the given <paramref name="name"/> that is defined into a Java
        /// properties file. 
        /// </summary>
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            IDictionary<string, CharSequence> data = new Dictionary<string, CharSequence>(PropertiesFileExtractor.DefaultKeyComparer);
            var propertyFile = context.Extract(_propertyFileName);
            switch (propertyFile)
            {
                case null:
                    return null;
                case TextDocumentResourceInfo jp:
                    foreach (var kvp in jp.Data)
                    {
                        data[kvp.Key] = kvp.Value;
                    }

                    break;
                //default:
                //    using (var stream = propertyResource.Value.Open())
                //    if (stream != null)
                //    {
                //        var x = new PropertiesFileExtractor();
                //        x.ReadData(stream, props = new Dictionary<string, string>(PropertiesFileExtractor.DefaultKeyComparer));
                //    }
                //    break;
            }

            if (data != null && data.TryGetValue($"{_keyPrefix}{name}", out var result))
            {
                // TODO: avoid calling `result.ToString()` on a CharSequence. Change resource info type appropriately
                return new TextResourceInfo(name, context.Culture, result.ToString(), _encoding);
            }

            return null;
        }
    }
}