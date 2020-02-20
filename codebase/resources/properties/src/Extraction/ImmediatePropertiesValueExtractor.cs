using System;
using Axle.Extensions.String;
using Axle.Resources.Extraction;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementation that can access the values defined in a Java properties file.
    /// </summary>
    internal sealed class ImmediatePropertiesValueExtractor : AbstractResourceExtractor
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
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            if (!GetPropertiesFileData(context.Location, out var propertyFileName, out var keyPrefix))
            {
                return null;
            }

            return new PropertiesValueExtractor(propertyFileName, keyPrefix).Extract(context, name);
        }
    }
}