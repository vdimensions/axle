using System;
using System.Collections.Generic;
using Axle.Extensions.String;
using Axle.References;
using Axle.Resources.Extraction;
using Axle.Resources.StructuredData;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementation that can access the values defined in a Java properties file.
    /// </summary>
    internal sealed class PropertiesValueExtractor : AbstractResourceExtractor
    {
        private static bool GetPropertiesFileData(Uri location, out string propertyFileName, out string keyPrefix)
        {
            propertyFileName = keyPrefix = null;
            const string ext = PropertiesResourceInfo.FileExtension;
            const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
            var locStr = location.ToString();
            keyPrefix = locStr.TakeAfterFirst(ext, cmp);
            propertyFileName = locStr.TakeBeforeFirst(keyPrefix, cmp);

            return !string.IsNullOrEmpty(propertyFileName) && propertyFileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        /// <summary>
        /// Attempts to locate a string value with the given <paramref name="name"/> that is defined into a Java properties file. 
        /// </summary>
        protected override Nullsafe<ResourceInfo> DoExtract(ResourceContext context, string name)
        {
            if (GetPropertiesFileData(context.Location, out var propertyFileName, out var keyPrefix))
            {
                IDictionary<string, string> props = null;
                var propertyResource = context.ExtractionChain.Extract(propertyFileName);
                if (!propertyResource.HasValue)
                {
                    return Nullsafe<ResourceInfo>.None;
                }
                switch (propertyResource.Value)
                {
                    case StructuredDataResourceInfo jp:
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

                if (props != null && props.TryGetValue($"{keyPrefix}{name}", out var result))
                {
                    return new TextResourceInfo(name, context.Culture, result);
                }
            }
            return Nullsafe<ResourceInfo>.None;
        }
    }
}