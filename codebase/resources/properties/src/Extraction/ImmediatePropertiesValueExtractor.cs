using System;
using System.Text;
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
            PropertiesValueExtractor.DeconstructPropertyFilePath(location.ToString(), out propertyFileName, out keyPrefix);

            return !string.IsNullOrEmpty(propertyFileName) 
                && propertyFileName.EndsWith(PropertiesResourceInfo.FileExtension, StringComparison.OrdinalIgnoreCase);
        }

        private readonly Encoding _encoding;

        public ImmediatePropertiesValueExtractor(Encoding encoding)
        {
            _encoding = encoding;
        }

        /// <inheritdoc />
        /// <summary>
        /// Attempts to locate a string value with the given <paramref name="name"/> that is defined into a Java properties file. 
        /// </summary>
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            if (!GetPropertiesFileData(context.Location, out var fileName, out var keyPrefix))
            {
                return null;
            }
            return new PropertiesValueExtractor(_encoding, fileName, keyPrefix).Extract(context, name);
        }
    }
}