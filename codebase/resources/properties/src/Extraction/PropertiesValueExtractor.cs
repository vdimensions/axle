using System.Collections.Generic;
using Axle.Resources.Extraction;
using Axle.Resources.Text.Data;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementation that can access the values defined in a Java properties file.
    /// </summary>
    public sealed class PropertiesValueExtractor : AbstractResourceExtractor
    {
        private readonly string _propertyFileName;
        private readonly string _keyPrefix;

        public PropertiesValueExtractor(string propertyFileName, string keyPrefix)
        {
            _propertyFileName = propertyFileName;
            _keyPrefix = keyPrefix;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Attempts to locate a string value with the given <paramref name="name"/> that is defined into a Java properties file. 
        /// </summary>
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            IDictionary<string, string> props = null;
            switch (context.Extract(_propertyFileName))
            {
                case null:
                    return null;
                case TextDataResourceInfo jp:
                    props = jp.Data;
                    break;
                default:
                    //using (var stream = propertyResource.Value.Open())
                    //if (stream != null)
                    //{
                    //    var x = new PropertiesFileExtractor();
                    //    x.ReadData(stream, props = new Dictionary<string, string>(PropertiesFileExtractor.DefaultKeyComparer));
                    //}
                    break;
            }

            if (props != null && props.TryGetValue($"{_keyPrefix}{name}", out var result))
            {
                return new TextResourceInfo(name, context.Culture, result);
            }

            return null;
        }
    }
}