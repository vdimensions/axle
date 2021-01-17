using System;
using Axle.Extensions.String;
using Axle.Extensions.Uri;
using Axle.Resources;
using Axle.Resources.Extraction;

namespace Axle.Application.Unity.Resources.Extraction
{
    public abstract class AbstractAssetExtractor : AbstractResourceExtractor
    {
        private readonly string _unityAssetsLookupDirectory;
        private readonly string _assetDirectoryIdentifierPrefix;
        private readonly string _pathRelativeToRoot;

        protected AbstractAssetExtractor(string unityAssetsLookupDirectory, string assetDirectoryIdentifierPrefix, string pathRelativeToRoot)
        {
            _unityAssetsLookupDirectory = unityAssetsLookupDirectory;
            _assetDirectoryIdentifierPrefix = assetDirectoryIdentifierPrefix;
            _pathRelativeToRoot = pathRelativeToRoot;
        }

        protected sealed override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var uriStr = context.Location.ToString().TrimStart("./");
            var ix = uriStr.IndexOf(_assetDirectoryIdentifierPrefix, StringComparison.OrdinalIgnoreCase);
            if (ix < 0)
            {
                return null;
            }

            var targetAssetPath = new Uri(_unityAssetsLookupDirectory + "/", UriKind.Absolute);
            var stuffAfterTargetDir = uriStr.Substring(ix + _assetDirectoryIdentifierPrefix.Length).TrimStart('/');
            var rootPath = stuffAfterTargetDir.Length > 0 ? targetAssetPath.Resolve(stuffAfterTargetDir) : targetAssetPath;
            var innerPath = new Uri(stuffAfterTargetDir.Length > 0 ? stuffAfterTargetDir : "./", UriKind.Relative);
            // TODO: detect resources location in path
            return DoExtract(context, rootPath, innerPath, name);
        }

        /// <summary>
        /// Performs the actual asset extraction.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="absolutePath">
        /// The absolute path to the resource object.
        /// </param>
        /// <param name="localPath">
        /// The local path of the resource object, relative to the special directory (Assets, StreamingAssets) it is being contained within.
        /// </param>
        /// <param name="name">
        /// The requested resource name.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the extracted resource, or null if the resource was not found. 
        /// </returns>
        protected abstract ResourceInfo DoExtract(IResourceContext context, Uri absolutePath, Uri localPath, string name);
    }
}
