using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Axle.Application.Unity.Resources.Extraction;
using Axle.Resources.Bundling;

namespace Axle.Application.Unity
{
    public sealed class UnityApplicationHost : AbstractUnityApplicationHost
    {
        static UnityApplicationHost()
        {
            var cmp = StringComparer.Ordinal;
            Profiles = UnityProfiles.Detect().ToArray();
            UnityEnvironmentName = Profiles.Contains(UnityProfiles.Editor, cmp)
                ? "Development"
                : string.Empty;
        }

        private static string UnityEnvironmentName  { get; }
        private static string[] Profiles { get; }

        #if !UNITY_WEBGL
        public static UnityApplicationHost Instance => Axle.References.Singleton<UnityApplicationHost>.Instance.Value;
        #else
        public static readonly UnityApplicationHost Instance = new UnityApplicationHost();
        #endif

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private UnityApplicationHost() : base(null, null, "host", UnityEnvironmentName, Profiles) { }
        
        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            bundle.Register(new Uri("./", UriKind.Relative))
                .Extractors.Register(new ResourcesFolderAssetExtractor());
        }

        protected override void SetupHostConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            bundle.Register(new Uri("./", UriKind.Relative))
                .Extractors.Register(new ResourcesFolderAssetExtractor());
        }
    }
}