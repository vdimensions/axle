#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using System.Globalization;
using System.IO;
using Axle.Extensions.Uri;
using Axle.Verification;

namespace Axle.Resources.FileSystem
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a resource located on the file system.
    /// </summary>
    public class FileSystemResourceInfo : ResourceInfo
    {
        private readonly Uri _location;

        /// <inheritdoc />
        internal FileSystemResourceInfo(Uri location, string name, CultureInfo culture) : this(location, name, culture, "application/octet-stream") { }
        internal FileSystemResourceInfo(Uri location, string name, CultureInfo culture, string contentType) : base(name, culture, contentType)
        {
            _location = location.VerifyArgument(nameof(location)).IsNotNull().Value;
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            if (!File.Exists(_location.AbsolutePath))
            {
                throw new ResourceNotFoundException(Name, Bundle, Culture);
            }
            try
            {
                return new FileStream(_location.AbsolutePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception e)
            {
                throw new ResourceLoadException(Name, Bundle, Culture, e);
            }
        }

        /// <inheritdoc />
        public override bool TryResolve(Type type, out object result)
        {
            if (type == typeof(FileInfo))
            {
                var file = new FileInfo(_location.AbsolutePath);
                result = file;
                return file.Exists;
            }
            return base.TryResolve(type, out result);
        }
    }
}
#endif