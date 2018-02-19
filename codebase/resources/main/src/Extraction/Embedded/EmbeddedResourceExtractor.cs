using System;
using System.Reflection;

using Axle.Environment;
using Axle.Extensions.String;
using Axle.Extensions.Uri;
using Axle.Verification;


namespace Axle.Resources.Extraction.Embedded
{
    /// <summary>
    /// An implementation of the <see cref="IResourceExtractor"/> interface that is capable of reading embedded resources.
    /// </summary>
    /// <seealso cref="EmbeddedResourceInfo"/>
    public class EmbeddedResourceExtractor : IResourceExtractor
    {
        private static Assembly GetAssembly(IRuntime runtime, Uri uri)
        {
            var assenblyName = uri.IsResource() ? uri.Host.TakeBeforeLast(".dll").TakeBeforeLast(".exe") : uri.Host;
            return runtime.LoadAssembly(assenblyName);
        }

        /// <inheritdoc />
        /// <summary>
        /// Attempts to read an embedded resource.
        /// </summary>
        public ResourceInfo Extract(ResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            var location = context.Location;
            var culture = context.Culture;

            if (location.IsAbsoluteUri && location.IsEmbeddedResource())
            {
                var runtime = Platform.Runtime;
                var assembly = GetAssembly(runtime, location);
                var actualAssembly = culture.Equals(System.Globalization.CultureInfo.InvariantCulture)
                    ? assembly
                    : runtime.LoadSatelliteAssembly(assembly, culture);
                /*
                 * Only create resource if there is a satellite assembly when the culture is not invariant.
                 * Also, never create an adapter if the assembly does not contain the requested resource.
                 */
                if (actualAssembly != null && EmbeddedResourceInfo.ContainsEmbeddedResource(actualAssembly, name))
                {
                    return new EmbeddedResourceInfo(actualAssembly, name, culture);
                }
            }
            #endif

            return null;
        }
    }
}