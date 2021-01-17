using System;
using Axle.Extensions.Uri;
using Axle.Resources;
using Axle.Resources.Extraction;
using UnityEngine.Networking;
#if !UNITY_WEBGL
using System.Threading;
#endif

namespace Axle.Application.Unity.Resources.Extraction
{
    public sealed class StreamingAssetExtractor : AbstractResourceExtractor
    {
        private const string StreamingAssetsPrefix = "StreamingAssets";

        // TODO: Async implementation
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var streamingAssetsUri = new Uri(global::UnityEngine.Application.streamingAssetsPath, UriKind.RelativeOrAbsolute);
            var culture = context.Culture;
            var dotIndex = name.IndexOf('.');
            var fileLookupName = string.IsNullOrEmpty(culture.Name)
                    ? name 
                    : dotIndex < 0 
                        ? $"{name}.{culture.Name}" 
                        : $"{name.Substring(0, dotIndex)}.{culture.Name}.{name.Substring(1 + dotIndex)}";

            var locStr = context.Location.ToString();
            var streamingAssetsIndex = locStr.IndexOf(StreamingAssetsPrefix, StringComparison.Ordinal);
            if (streamingAssetsIndex >= 0)
            {
                locStr = locStr.Substring(streamingAssetsIndex + StreamingAssetsPrefix.Length);
            }
            if (locStr.Length > 0)
            {
                streamingAssetsUri = streamingAssetsUri.Resolve(locStr);
            }
            var streamingAssetPath = streamingAssetsUri.Resolve($"./{fileLookupName}").ToString();
            var handler = new DownloadHandlerBuffer();
            var www = new UnityWebRequest(streamingAssetPath, "GET", handler, null);
            var asyncResult = www.SendWebRequest();
            #if !UNITY_WEBGL
            SpinWait.SpinUntil(() => asyncResult.isDone);
            #else
            // TODO: check if we can avoid the imminent CPU spike this code would cause
            do { } while (!asyncResult.isDone);
            #endif
            if (!string.IsNullOrEmpty(www.error))
            {
                return null;
            }

            var data = handler.data;
            if (data == null)
            {
                return null;
            }
            return new BinaryResourceInfo(name, context.Culture, data);
        }
    }
}