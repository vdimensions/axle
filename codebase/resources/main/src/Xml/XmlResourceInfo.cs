using System.Globalization;
using System.IO;
using System.Xml;
#if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
using System;
using Axle.IO.Serialization;
#endif

namespace Axle.Resources.Xml
{
    /// <summary>
    /// An abstract class to represent a XML resource.
    /// </summary>
    public abstract class XmlResourceInfo : ResourceInfo
    {
        /// <summary>
        /// Gets the default content (MIME) type of an XML files.
        /// </summary>
        new public const string ContentType = "application/xml";
        /// <summary>
        /// Gets the default (lowercase) file extension for XML files.
        /// </summary>
        public const string FileExtension = ".xml";

        private readonly ResourceInfo _originalResource;

        /// <summary>
        /// Creates a new instance of the <see cref="XmlResourceInfo"/> class.
        /// </summary>
        /// <param name="name">
        /// The unique name of the resource within the current resource bundle.
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> for which the resource was requested. 
        /// </param>
        protected XmlResourceInfo(string name, CultureInfo culture) : base(name, culture, ContentType) { }
        /// <summary>
        /// Creates a new instance of the <see cref="XmlResourceInfo"/> class.
        /// </summary>
        /// <param name="name">
        /// The unique name of the resource within the current resource bundle.
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> for which the resource was requested. 
        /// </param>
        /// <param name="originalResource">
        /// A reference to the original <see cref="ResourceInfo"/> the current <see cref="XmlResourceInfo"/>
        /// implementation was streamed from.
        /// </param>
        protected XmlResourceInfo(string name, CultureInfo culture, ResourceInfo originalResource) : this(name, culture)
        {
            _originalResource = originalResource;
        }

        /// <inheritdoc />
        /// <summary>
        /// Opens a new <see cref="Stream"/> to read the xml contents represented by the current
        /// <see cref="XmlResourceInfo"/> implementation. 
        /// </summary>
        public sealed override Stream Open()
        {
            // ReSharper disable EmptyGeneralCatchClause
            try
            {
                var result = _originalResource?.Open();
                if (result != null)
                {
                    return result;
                }
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            var stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { CloseOutput = false }))
            {
                WriteTo(writer);
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
        /// <inheritdoc />
        public override bool TryResolve(Type type, out object result)
        {
            var serializer = new XmlSerializer();
            try
            {
                using (var stream = Open())
                {
                    result = serializer.Deserialize(stream, type);
                }

                return true;
            }
            catch
            {
                if (base.TryResolve(type, out result))
                {
                    return true;
                }
                result = null;
                return false;
            }            
        }
        #endif

        /// <summary>
        /// Write the represented XML document to an <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        /// An <see cref="XmlWriter"/> into which this method will write.
        /// </param>
        protected abstract void WriteTo(XmlWriter writer);
    }
}