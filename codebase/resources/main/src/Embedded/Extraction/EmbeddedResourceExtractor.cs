#if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
using System;
using System.Reflection;
using Axle.Environment;
using Axle.Extensions.String;
using Axle.Extensions.Uri;
using Axle.Resources.Extraction;
using Axle.Verification;

namespace Axle.Resources.Embedded.Extraction
{
    #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
    /// <summary>
    /// An implementation of the <see cref="IResourceExtractor"/> interface that is capable of reading embedded
    /// resources.
    /// </summary>
    /// <seealso cref="EmbeddedResourceInfo"/>
    #else
    /// <summary>
    /// An implementation of the <see cref="IResourceExtractor"/> interface that is capable of reading embedded
    /// resources.
    /// </summary>
    #endif
    public class EmbeddedResourceExtractor : AbstractResourceExtractor
    {
        #if NETSTANDARD1_0_OR_NEWER || NETFRAMEWORK
        private static Assembly GetAssembly(IRuntime runtime, Uri uri)
        {
            const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
            var assemblyName = uri.IsResource() ? uri.Host.TrimEnd(".dll", cmp).TrimEnd(".exe", cmp) : uri.Host;
            return runtime.LoadAssembly(assemblyName);
        }
        #endif

        /// <summary>
        /// Attempts to read an embedded resource.
        /// </summary>
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            var location = context.Location;
            var culture = context.Culture;

            if (Accepts(location))
            {
                var runtime = Platform.Runtime;
                var assembly = GetAssembly(runtime, location);
                var actualName = location.Resolve(name).AbsolutePath.TrimStart('/');
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                var actualAssembly = culture.Equals(System.Globalization.CultureInfo.InvariantCulture)
                    ? assembly
                    : runtime.LoadSatelliteAssembly(assembly, culture);
                #else
                var actualAssembly = assembly;
                #endif
                /*
                 * Only create resource if there is a satellite assembly when the culture is not invariant.
                 * Also, never create an adapter if the assembly does not contain the requested resource.
                 */
                if (actualAssembly != null && EmbeddedResourceInfo.ContainsEmbeddedResource(actualAssembly, actualName))
                {
                    return new EmbeddedResourceInfo(actualAssembly, name, actualName, culture);
                }
            }

            return null;
        }

        /// <inheritdoc />
        protected override bool Accepts(Uri location) => location.IsAbsoluteUri && location.IsEmbeddedResource();
    }
}
#endif