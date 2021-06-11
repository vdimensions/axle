using System;
using System.Collections.Generic;
using System.Text;
using Axle.Extensions.String;
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
        internal static void DeconstructPropertyFilePath(string propertyFilePath, out string propertyFileName, out string keyPrefix)
        {
            propertyFileName = keyPrefix = null;
            const string ext = PropertiesResourceInfo.FileExtension;
            const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
            keyPrefix = propertyFilePath.TakeAfterLast(ext, cmp);
            propertyFileName = propertyFilePath.TakeBeforeLast(keyPrefix, cmp);
            const char slash = '/';
            keyPrefix = keyPrefix.TrimStart(slash);
            if (keyPrefix.EndsWith(slash.ToString()))
            {
                keyPrefix = $"{keyPrefix.TrimEnd(slash)}.";
            }
        }
        
        private readonly Encoding _encoding;
        private readonly string _propertyFileName;
        private readonly string _keyPrefix;

        public PropertiesValueExtractor(Encoding encoding, string propertyFileName, string keyPrefix)
        {
            _encoding = encoding;
            DeconstructPropertyFilePath(propertyFileName, out _propertyFileName, out var prefix);
            _keyPrefix = string.IsNullOrEmpty(prefix) ? keyPrefix : string.Concat(prefix, keyPrefix);
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

            if (data.Count > 0 && data.TryGetValue($"{_keyPrefix}{name}", out var result))
            {
                // TODO: avoid calling `result.ToString()` on a CharSequence. Change resource info type appropriately
                return new TextResourceInfo(name, context.Culture, result.ToString(), _encoding);
            }

            return null;
        }
    }
}