#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;
using Axle.Environment;
#if !UNITY_WEBGL
using Axle.References;
#endif
using Axle.Resources.Bundling;

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
        
        #if !UNITY_WEBGL
        /// <summary>
        /// Returns the sole instance of the <see cref="DefaultApplicationHost"/> class.
        /// </summary>
        public static DefaultApplicationHost Instance => Singleton<DefaultApplicationHost>.Instance;
        #else
        public static readonly DefaultApplicationHost Instance = new DefaultApplicationHost();
        #endif

        private DefaultApplicationHost() : base(null, null, InferredEnvironmentName) { }

        /// <inheritdoc />
        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }
    }
}
#endif