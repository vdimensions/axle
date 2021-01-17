using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Axle.Extensions.String;
using Axle.Extensions.Uri;
using Axle.Resources;
using Axle.Resources.Extraction;

namespace Axle.Application.Unity.Resources.Extraction
{
    public sealed class ResourcesFolderAssetExtractor : PathForwardingResourceExtractor
    {
        private const string ResourcesPrefix = "Resources";
        
        private sealed class ActualResourcesFolderAssetExtractor : AbstractResourceExtractor
        {
            private static bool IsVerbatim(IDictionary<string, string> query)
            {
                var valueComparer = StringComparer.OrdinalIgnoreCase;
                return query.TryGetValue("verbatim", out var value) && valueComparer.Equals(value, "true");
            }
            private static bool UseCulture(IDictionary<string, string> query)
            {
                var valueComparer = StringComparer.OrdinalIgnoreCase;
                return !query.TryGetValue("use_culture", out var value) || valueComparer.Equals(value, "true");
            }

            protected override ResourceInfo DoExtract(IResourceContext context, string name)
            {
                var culture = context.Culture;
                var location = context.Location;
                // check if we have specified custom query parameters to instruct the resource loading
                var query = location.GetQueryParameters();
                var isVerbatim = IsVerbatim(query);
                var useCulture = UseCulture(query);
                // remove any query string from the lookup path
                location = new Uri(location.OriginalString.TakeBeforeFirst('?'), UriKind.RelativeOrAbsolute);
                
                var fragments = location.Resolve(name).Normalize().ToString().Split('/');
                var skipCount = fragments.TakeWhile(x => !x.Equals(ResourcesPrefix, StringComparison.OrdinalIgnoreCase)).Count();
                if (fragments.Length == skipCount)
                {
                    return null;
                }
                var pathAfterResourceDir = 
                    fragments.Skip(skipCount+1).Aggregate(new StringBuilder(), (sb, a) => sb.Append('/').Append(a))
                    .ToString().Substring(1);
                var resourcePath = pathAfterResourceDir;
                var resourceFileNameNoExt = isVerbatim 
                    ? Path.GetFileName(resourcePath) 
                    : Path.GetFileNameWithoutExtension(resourcePath);
                var resourceName = useCulture && !string.IsNullOrEmpty(culture.Name)
                    ? $"{resourceFileNameNoExt}.{culture.Name}"
                    : resourceFileNameNoExt;
                var unityResourceName = Path.Combine(
                    Path.GetDirectoryName(resourcePath), 
                    resourceName);
                var resourceObject = UnityEngine.Resources.Load(unityResourceName);
                
                return resourceObject == null 
                    ? null
                    : UnityAssetResourceInfo.Create(name, culture, resourceObject);
            }
        }
        
        public ResourcesFolderAssetExtractor() : base(new ActualResourcesFolderAssetExtractor()) { }

        protected override Uri GetForwardedLocation(Uri location)
        {
            if (location.IsAbsoluteUri)
            {
                return location;
            }
            return new Uri($"{ResourcesPrefix}/", UriKind.Relative).Resolve(location);
        }

    }
}