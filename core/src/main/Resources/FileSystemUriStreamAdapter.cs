#if !NETSTANDARD || NETSTANDARD1_5_OR_NEWER
using System;
using System.IO;

using Axle.Extensions.Uri;
using Axle.Verification;


namespace Axle.Resources
{
    public sealed class FileSystemUriStreamAdapter : IUriStreamAdapter
    {
        public bool CanHandle(Uri uri)
        {
            return uri != null && uri.IsFile();
        }

        public Stream GetStream(Uri uri)
        {
            uri.VerifyArgument(nameof(uri))
               .IsNotNull()
               .IsTrue(u => u.IsAbsoluteUri, string.Format("The provided uri `{0}` must be absolute. ", uri)) 
               .IsTrue(CanHandle, string.Format("The provided uri `{0}` is not a valid file uri. ", uri));
            return new FileStream(uri.AbsolutePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
#endif