using System;
using System.Globalization;


namespace Axle.Resources.Extraction.FileSystem
{
    public class FileSystemResourceExtractor : AbstractStreamableResourceExtractor
    {
        //public FileSystemResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy)
        //{
        //}

        protected override bool TryGetStreamAdapter(Uri location, CultureInfo culture, string name, out IUriStreamAdapter adapter)
        {
            #if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
            if (location.IsAbsoluteUri && location.IsFile && CultureInfo.InvariantCulture.Equals(culture))
            {
                // Filesystem resources are considered culture-invariant.
                //
                adapter = new FileSystemUriStreamAdapter();
                return true;
            }
            #endif

            adapter = null;
            return false;
        }
    }
}