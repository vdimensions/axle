using System;
using System.Drawing;
using System.Globalization;
using System.IO;

using Axle.Verification;


namespace Axle.Resources
{
    public sealed class IconResourceInfo : ResourceInfo
    {
        private readonly Icon _icon;

        public IconResourceInfo(Uri key, CultureInfo culture, Icon icon) : base(key, culture, "image/x-icon")
        {
            _icon = icon.VerifyArgument(nameof(icon)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            var result = new MemoryStream();
            _icon.Save(result);
            return result;
        }

        public Icon Value => _icon;
    }
}