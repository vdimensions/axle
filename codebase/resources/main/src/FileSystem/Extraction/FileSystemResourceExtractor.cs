#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using System.Globalization;
using System.IO;
using Axle.Extensions.Uri;
using Axle.Resources.Extraction;
using Axle.Verification;

namespace Axle.Resources.FileSystem.Extraction
{
    /// <summary>
    /// An implementation of the <see cref="IResourceExtractor"/> that reads resources from the file system.
    /// </summary>
    public class FileSystemResourceExtractor : AbstractResourceExtractor
    {
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            var location = context.Location.Resolve(name);
            var culture = context.Culture;
            if (CultureInfo.InvariantCulture.Equals(culture) && File.Exists(location.AbsolutePath))
            {
                return new FileSystemResourceInfo(context.Location, name, culture);
            }
            #endif

            return null;
        }

        protected override bool Accepts(Uri location) => location.IsAbsoluteUri && location.IsFile();
    }
}
#endif