using System.Globalization;
using System.IO;


namespace Axle.Resources.Xml
{
    public abstract class XmlResourceInfo : ResourceInfo
    {
        new public const string ContentType = "application/xml";
        public const string FileExtension = ".xml";

        private readonly ResourceInfo _originalResource;

        protected XmlResourceInfo(string name, CultureInfo culture) : base(name, culture, ContentType) { }
        protected XmlResourceInfo(string name, CultureInfo culture, ResourceInfo originalResource) : this(name, culture)
        {
            _originalResource = originalResource;
        }

        public sealed override Stream Open()
        {
            try
            {
                var result = _originalResource?.Open();
                if (result != null)
                {
                    return result;
                }
            }
            catch { }

            return DoOpen();
        }

        protected abstract Stream DoOpen();
    }
}