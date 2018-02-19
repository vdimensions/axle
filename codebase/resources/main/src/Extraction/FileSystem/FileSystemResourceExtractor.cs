using System.Globalization;
using System.IO;

using Axle.Extensions.Uri;
using Axle.Verification;


namespace Axle.Resources.Extraction.FileSystem
{
    /// <summary>
    /// An implementation of the <see cref="IResourceExtractor"/> that reads resources from the file system.
    /// </summary>
    public class FileSystemResourceExtractor : IResourceExtractor
    {
        /// <inheritdoc />
        public ResourceInfo Extract(ResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            #if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
            var location = context.Location.Resolve(name);
            var culture = context.Culture;
            if (CultureInfo.InvariantCulture.Equals(culture) && File.Exists(location.AbsolutePath))
            {
                return new FileSystemResourceInfo(location, name, culture);
            }
            #endif

            return null;
        }
    }
}