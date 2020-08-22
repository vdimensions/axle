using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Axle.Resources.Extraction;
using Axle.Resources.Text.Documents;
using Axle.Text;
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

        public PropertiesFileExtractor(Encoding encoding) : base(encoding) { }
            
        protected override ITextDocumentReader GetReader(StringComparer comparer) 
            => new PropertiesDocumentReader(comparer);

        protected override TextDocumentResourceInfo CreateResourceInfo(
            string name, 
            CultureInfo culture, 
            IDictionary<string, CharSequence> data)
        {
            return new PropertiesResourceInfo(name, culture, data);
        }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}