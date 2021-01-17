using System;
using System.Linq;
using Axle.Application.Unity.Resources.Extraction;
using Axle.Resources.Bundling;

namespace Axle.Application.Unity.Editor
{
    public sealed class UnityEditorApplicationHost : AbstractUnityApplicationHost
    {
        private static readonly string[] _profiles;
    
        static UnityEditorApplicationHost()
        {
            var profiles = UnityProfiles.Detect().ToArray();
        
            if (!UnityEngine.Application.isEditor || UnityEngine.Application.isPlaying || UnityEditor.EditorApplication.isPlaying || UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // do not initialize plugins when the editor is playing.
                profiles = profiles.Except(new[]{UnityProfiles.Editor}, StringComparer.Ordinal).ToArray();
            }

            _profiles = profiles;
        }

        #if !UNITY_WEBGL
        public static Host Instance => Singleton<UnityEditorApplicationHost>.Instance.Value;
        #else
        public static readonly UnityEditorApplicationHost Instance = new UnityEditorApplicationHost();
        #endif
    
        private UnityEditorApplicationHost() : base(null, "unityeditor", "host.unityeditor", UnityProfiles.Editor, _profiles) { }

        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            bundle.Register(new Uri($"./", UriKind.Relative)).Extractors
                .Register(new ResourcesFolderAssetExtractor())
                .Register(new StreamingAssetExtractor());
        }

        protected override void SetupHostConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            bundle.Register(new Uri($"./", UriKind.Relative)).Extractors
                .Register(new ResourcesFolderAssetExtractor())
                .Register(new StreamingAssetExtractor());
        }
    }
}