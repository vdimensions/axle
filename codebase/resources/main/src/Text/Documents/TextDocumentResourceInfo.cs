using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Axle.Resources.Text.Documents
{
    /// <summary>
    /// A class representing a Java properties file as a resource.
    /// </summary>
    public abstract class TextDocumentResourceInfo : ResourceInfo
    {
        private readonly IDictionary<string, string> _data;
        private readonly ResourceInfo _originalResource;

        protected TextDocumentResourceInfo(
                string name, 
                CultureInfo culture, 
                string mimeType, 
                IDictionary<string, string> data) 
            : base(name, culture, mimeType)
        {
            _data = data;
        }
        protected TextDocumentResourceInfo(
                string name, 
                CultureInfo culture, 
                string mimeType, 
                IDictionary<string, string> data, 
                ResourceInfo originalResource) 
            : this(name, culture, mimeType, data)
        {
            _originalResource = originalResource;
        }

        /// <summary>
        /// Opens a <see cref="Stream">stream</see> object for reading the contents of the properties file.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream">stream</see> object for reading the contents of the properties file.
        /// </returns>
        public override Stream Open() => _originalResource?.Open();

        public override bool TryResolve(Type targetType, out object result)
        {
            if (targetType == typeof(IDictionary<string, string>))
            {
                result = Data;
                return true;
            }
            
            return base.TryResolve(targetType, out result);
        }

        /// <summary>
        /// Gets a <see cref="IDictionary{TKey,TValue}"/> representing the contents of the properties file.
        /// </summary>
        //TODO: make the data read-only; pass appropriate comparer
        public IDictionary<string, string> Data => new Dictionary<string, string>(_data);
    }
}