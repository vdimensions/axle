#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System;
using System.IO;

using Axle.Extensions.Uri;
using Axle.Verification;


namespace Axle.Resources.Extraction.Streaming
{
    /// <summary>
    /// An implementation of <see cref="IUriStreamAdapter"/> that can handle filesystem locations.
    /// </summary>
    public sealed class FileSystemUriStreamAdapter : IUriStreamAdapter
    {
        internal bool CanHandle(Uri uri)
        {
            return uri != null && uri.IsFile();
        }

        /// <inheritdoc />
        public Stream GetStream(Uri uri)
        {
            uri.VerifyArgument(nameof(uri))
               .IsNotNull()
               .IsTrue(u => u.IsAbsoluteUri, string.Format("The provided uri `{0}` must be absolute. ", uri)) 
               .IsTrue(CanHandle, string.Format("The provided uri `{0}` is not a valid file uri. ", uri));
            var location = uri.AbsolutePath;
            if (!File.Exists(location))
            {
                return null;
            }
            return new FileStream(uri.AbsolutePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
#endif