#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.References;
using Axle.Resources.Bundling;

namespace Axle
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
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                var dotnetEnv = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT", EnvironmentVariableTarget.Process);
                #else
                var dotnetEnv = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                #endif
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
        
        public static DefaultApplicationHost Instance => Singleton<DefaultApplicationHost>.Instance;

        private DefaultApplicationHost() : base(null, null, InferredEnvironmentName) { }

        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }
    }
}
#endif