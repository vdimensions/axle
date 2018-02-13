#if !NETSTANDARD
using System;
using System.Drawing;
using System.Globalization;
using System.IO;

using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// A resource representing an icon object.
    /// </summary>
    public sealed class IconResourceInfo : ResourceInfo
    {
        private readonly Icon _icon;

        public IconResourceInfo(string name, CultureInfo culture, Icon icon) : base(name, culture, "image/x-icon")
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

        /// <summary>
        /// Gets a reference to the <see cref="Icon"/> object represented by the current <see cref="IconResourceInfo" /> instance.
        /// </summary>
        public Icon Value => _icon;
    }
}
#endif