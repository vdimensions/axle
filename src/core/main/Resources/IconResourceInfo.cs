using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mime;

using Axle.Verification;


namespace Axle.Resources
{
    public sealed class IconResourceInfo : ResourceInfo
    {
        private readonly Icon icon;

        public IconResourceInfo(Uri key, CultureInfo culture, Icon icon) : base(key, culture, new ContentType("image/x-icon"))
        {
            this.icon = icon.VerifyArgument(nameof(icon)).IsNotNull();
        }

        public override Stream Open()
        {
            var result = new MemoryStream();
            icon.Save(result);
            return result;
        }

        public Icon Value => icon;
    }
}