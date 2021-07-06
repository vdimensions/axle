using System.Reflection;
using Axle.Extensions.Uri;
using Axle.Verification;
using UriParser = Axle.Text.Parsing.UriParser;

namespace Axle.Resources.Bundling
{
    /// <summary>
    /// A static class containing various resource bundle extension methods.
    /// </summary>
    public static class ResourceBundleExtensions
    {
        /// <summary>
        /// Registers an assembly as a source for embedded resources lookup.
        /// </summary>
        /// <param name="bundleContent">
        /// An <see cref="IConfigurableBundleContent"/> instance representing the resource bundle being configured.
        /// </param>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> object that will provide its embedded resources for lookup by the current bundle.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IConfigurableBundleContent"/> instance. 
        /// </returns>
        public static IConfigurableBundleContent Register(this IConfigurableBundleContent bundleContent, Assembly assembly)
        {
            return Register(bundleContent, assembly, string.Empty);
        }
        /// <summary>
        /// Registers an assembly as a source for embedded resources lookup.
        /// </summary>
        /// <param name="bundleContent">
        /// An <see cref="IConfigurableBundleContent"/> instance representing the resource bundle being configured.
        /// </param>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> object that will provide its embedded resources for lookup by the current bundle.
        /// </param>
        /// <param name="path">
        /// An optional sub-path within the embedded resources directory of the provided assembly.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IConfigurableBundleContent"/> instance. 
        /// </returns>
        public static IConfigurableBundleContent Register(this IConfigurableBundleContent bundleContent, Assembly assembly, string path)
        {
            bundleContent.VerifyArgument(nameof(bundleContent)).IsNotNull();
            assembly.VerifyArgument(nameof(assembly)).IsNotNull();

            var uriParser = new UriParser();
            var assemblyUri = uriParser.Parse($"assembly://{assembly.GetName().Name}/");
            if (!string.IsNullOrEmpty(path))
            {
                assemblyUri = assemblyUri.Resolve(path);
            }
            return bundleContent.Register(assemblyUri);
        }

        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Registers an application-relative path (a path relative to the current executable location) for resource lookup.
        /// </summary>
        /// <param name="bundleContent">
        /// An <see cref="IConfigurableBundleContent"/> instance representing the resource bundle being configured.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IConfigurableBundleContent"/> instance. 
        /// </returns>
        public static IConfigurableBundleContent RegisterApplicationRelativePath(this IConfigurableBundleContent bundleContent)
        {
            return RegisterApplicationRelativePath(bundleContent, string.Empty);
        }
        /// <summary>
        /// Registers an application-relative path (a path relative to the current executable location) for resource lookup.
        /// </summary>
        /// <param name="bundleContent">
        /// An <see cref="IConfigurableBundleContent"/> instance representing the resource bundle being configured.
        /// </param>
        /// <param name="path">
        /// An optional sub-path within the current executable location directory that should be used for the resource lookup.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IConfigurableBundleContent"/> instance. 
        /// </returns>
        public static IConfigurableBundleContent RegisterApplicationRelativePath(this IConfigurableBundleContent bundleContent, string path)
        {
            var executingAssembly = 
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            #else
                Assembly.GetEntryAssembly();
            #endif
            var uri = new UriParser().Parse(executingAssembly.Location).Resolve("../");
            if (!string.IsNullOrEmpty(path))
            {
                uri = uri.Resolve(path);
            }
            return bundleContent.Register(uri);
        }
        #endif
    }
}