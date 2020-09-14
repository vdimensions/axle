#if NETFRAMEWORK
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
        /// <summary>
        /// The standard MIME type used for icon objects.
        /// </summary>
        new public const string ContentType = "image/x-icon";

        private readonly Icon _icon;

        /// <summary>
        /// Creates a new instance of the <see cref="IconResourceInfo" /> class.
        /// </summary>
        /// <param name="name">
        /// The name of the resource.
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> for which the resource was requested. 
        /// </param>
        /// <param name="icon">
        /// The actual <see cref="Icon">icon</see> object represented by the current <see cref="IconResourceInfo"/> instance.
        /// </param>
        public IconResourceInfo(string name, CultureInfo culture, Icon icon) : base(name, culture, ContentType)
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

        /// <inheritdoc />
        public override bool TryResolve(Type type, out object result)
        {
            if (type == typeof(Image))
            {
                return (result = _icon) != null;
            }
            return base.TryResolve(type, out result);
        }

        /// <summary>
        /// Gets a reference to the <see cref="Icon"/> object represented by the current <see cref="IconResourceInfo" /> instance.
        /// </summary>
        public Icon Value => _icon;
    }
}
#endif