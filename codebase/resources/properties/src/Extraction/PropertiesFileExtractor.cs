using System;
using System.Collections.Generic;
using System.Globalization;
using Axle.Resources.Extraction;
using Axle.Resources.Text.Documents;
using Axle.Text.Documents;
using Axle.Text.Documents.Properties;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementations capable of creating Java properties files.
    /// </summary>
    internal sealed class PropertiesFileExtractor : AbstractTextDocumentExtractor
    {
        internal static readonly StringComparer DefaultKeyComparer = StringComparer.OrdinalIgnoreCase;
            
        protected override ITextDocumentReader GetReader(StringComparer comparer) 
            => new PropertiesDataReader(comparer);

        protected override TextDocumentResourceInfo CreateResourceInfo(
            string name, 
            CultureInfo culture, 
            IDictionary<string, string> data)
        {
            return new PropertiesResourceInfo(name, culture, data);
        }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}