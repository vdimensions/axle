using System;
using System.Globalization;
using System.IO;
using System.Net.Mime;

using Axle.Verification;


namespace Axle.Resources
{
    public abstract class ResourceInfo
    {
        protected ResourceInfo(Uri key, CultureInfo culture, ContentType contentType)
        {
            Key = key.VerifyArgument(nameof(key)).IsNotNull();
            Culture = culture.VerifyArgument(nameof(culture)).IsNotNull();
            ContentType = contentType.VerifyArgument(nameof(contentType)).IsNotNull();
        }
        protected ResourceInfo(Uri key, CultureInfo culture, string contentTypeName)
            : this(key, culture, new ContentType(contentTypeName.VerifyArgument(nameof(contentTypeName)).IsNotNullOrEmpty())) { }

        public abstract Stream Open();

        public Uri Key { get; }
        public CultureInfo Culture { get; }
        public ContentType ContentType { get; }
    }
}