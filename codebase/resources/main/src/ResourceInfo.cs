using System;
using System.Globalization;
using System.IO;
//using System.Net.Mime;

using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// An abstract class representing a resource object. 
    /// </summary>
    public abstract class ResourceInfo
    {
        //protected ResourceInfo(Uri location, CultureInfo culture, ContentType contentType)
        protected ResourceInfo(Uri location, string name, CultureInfo culture, string contentType)
        {
            Location = location.VerifyArgument(nameof(location)).IsNotNull();
            Name = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            Culture = culture.VerifyArgument(nameof(culture)).IsNotNull();
            ContentType = contentType.VerifyArgument(nameof(contentType)).IsNotNull();
        }
        //protected ResourceInfo(Uri location, CultureInfo culture, string contentTypeName)
        //    : this(location, culture, new ContentType(contentTypeName.VerifyArgument(nameof(contentTypeName)).IsNotNullOrEmpty())) { }

        /// <summary>
        /// Opens a new <see cref="Stream"/> to the represented resource's data.
        /// </summary>
        /// <returns>
        /// A new <see cref="Stream"/> to the represented resource's data.
        /// </returns>
        public abstract Stream Open();

        /// <summary>
        /// An <see cref="Uri"/> that unuquely identifies the resource object represented by this <see cref="ResourceInfo"/> instance.
        /// </summary>
        /// <seealso cref="System.Uri"/>
        public Uri Location { get; }

        /// <summary>
        /// Gets a <see cref="string"/> value that indicates the resource bundle containing the current resource object.
        /// </summary>
        public string Bundle { get; internal set; }

        /// <summary>
        /// Gets a <see cref="string"/> value that uniquely identifies the resource within its bundle.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A <see cref="CultureInfo"/> object representing the culture this <see cref="ResourceInfo"/> instance is defined for.
        /// <remarks>
        /// This property returns the actual <see cref="CultureInfo"/> of the resource, regardless if it has been requested with a more-specific culture.
        /// </remarks>
        /// </summary>
        public CultureInfo Culture { get; }

        ///// <summary>
        ///// Gets the <see cref="System.Net.Mime.ContentType"/> header describing the represented resource.
        ///// </summary>
        ///// <seealso cref="System.Net.Mime.ContentType"/>
        //public ContentType ContentType { get; }
        /// <summary>
        /// Gets the content type header describing the represented resource.
        /// </summary>
        public string ContentType { get; }
    }
}