using System;
using System.Collections.Generic;
using System.Globalization;
using Axle.Resources.Extraction;
using Axle.Resources.StructuredData;
using Axle.Text.StructuredData;
using Axle.Text.StructuredData.Properties;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementations capable of creating Java properties files.
    /// </summary>
    internal sealed class PropertiesFileExtractor : AbstractStructuredDataExtractor
    {
        internal static readonly StringComparer DefaultKeyComparer = StringComparer.OrdinalIgnoreCase;
            
        protected override IStructuredDataReader GetReader(StringComparer comparer) 
            => new PropertiesDataReader(comparer);

        protected override StructuredDataResourceInfo CreateResourceInfo(
            string name, 
            CultureInfo culture, 
            IDictionary<string, string> data)
        {
            return new PropertiesResourceInfo(name, culture, data);
        }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}