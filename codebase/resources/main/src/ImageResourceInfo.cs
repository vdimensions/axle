#if !NETSTANDARD
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// A resource representing an image object.
    /// </summary>
    public sealed class ImageResourceInfo : ResourceInfo
    {
        private readonly Image _image;

        private static string GetContentType(Image image)
        {
            if (image == null)
            {
                return null;
            }
            var codec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == image.RawFormat.Guid);
            return codec.MimeType;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ImageResourceInfo" /> class.
        /// </summary>
        /// <param name="name">
        /// The name of the resource.
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> for which the resource was requested. 
        /// </param>
        /// <param name="image">
        /// The actual <see cref="Image">image</see> object represented by the current <see cref="ImageResourceInfo"/> instance.
        /// </param>
        public ImageResourceInfo(string name, CultureInfo culture, Image image) : base(name, culture, GetContentType(image))
        {
            _image = image.VerifyArgument(nameof(image)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            var result = new MemoryStream();
            _image.Save(result, _image.RawFormat);
            return result;
        }

        /// <summary>
        /// Gets a reference to the <see cref="Image"/> object represented by the current <see cref="ImageResourceInfo" /> instance.
        /// </summary>
        public Image Value => _image;
    }
}
#endif