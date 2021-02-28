#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Environment;
using Axle.References;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.ResX.Extraction;
#if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
using Axle.Resources.Embedded.Extraction;
#endif
#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using Axle.Resources.FileSystem.Extraction;
#endif

namespace Axle.Application
{
    /// <summary>
    /// The default <see cref="IApplicationHost"/> implementation. 
    /// </summary>
    public sealed class DefaultApplicationHost : AbstractApplicationHost
    {
        private static string InferredEnvironmentName
        {
            get
            {
                var dotnetEnv = Platform.Environment["DOTNET_ENVIRONMENT"];
                if (!string.IsNullOrEmpty(dotnetEnv))
                {
                    return dotnetEnv;
                }
                #if DEBUG
                return "Debug";
                #endif
                return string.Empty;
            }
        }
        
        /// <summary>
        /// Returns the sole instance of the <see cref="DefaultApplicationHost"/> class.
        /// </summary>
        public static DefaultApplicationHost Instance => Singleton<DefaultApplicationHost>.Instance;

        private DefaultApplicationHost() : base(null, null, InferredEnvironmentName) { }

        public override IResourceExtractorRegistry ConfigureDefaultResourcePaths(IResourceExtractorRegistry extractors)
        {
            return extractors
                #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
                .Register(new FileSystemResourceExtractor())
                #endif
                .Register(new ResXResourceExtractor())
                #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
                .Register(new EmbeddedResourceExtractor())
                #endif
                ;
        }

        /// <inheritdoc />
        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }
    }
}
#endif