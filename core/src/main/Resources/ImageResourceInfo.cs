#if !NETSTANDARD
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources
{
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

        public ImageResourceInfo(Uri key, CultureInfo culture, Image image) : base(key, culture, GetContentType(image))
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

        public Image Value => _image;
    }
}
#endif