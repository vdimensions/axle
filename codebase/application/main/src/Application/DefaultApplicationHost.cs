#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Environment;
using Axle.References;
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
        
        /// <summary>
        /// Returns the sole instance of the <see cref="DefaultApplicationHost"/> class.
        /// </summary>
        public static DefaultApplicationHost Instance => Singleton<DefaultApplicationHost>.Instance;

        private DefaultApplicationHost() : base(null, null, InferredEnvironmentName) { }

        /// <inheritdoc />
        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }
    }
}
#endif