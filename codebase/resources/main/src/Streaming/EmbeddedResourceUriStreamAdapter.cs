#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using Axle.Environment;
using Axle.Extensions.String;
using Axle.Extensions.Uri;
using Axle.Globalization;
using Axle.Verification;


namespace Axle.Resources.Streaming
{
    /// <summary>
    /// An implementation of <see cref="IUriStreamAdapter"/> that deals with embedded resources.
    /// </summary>
    public sealed class EmbeddedResourceUriStreamAdapter : IUriStreamAdapter
    {
        internal static Assembly GetAssembly(IRuntime runtime, Uri uri)
        {
            uri.VerifyArgument(nameof(uri))
                .IsNotNull()
                .IsTrue(x => x.IsAbsoluteUri, "Provided uri is not an absolute uri.")
                .IsTrue(x => x.IsEmbeddedResource(), $"Provided uri does not have a valid schema that suggests it is an assebly uri. Assembly uri have one of the following shcemas: {UriExtensions.UriSchemeAssembly} or {UriExtensions.UriSchemeResource}");
            var assenblyName = uri.IsResource() ? uri.Host.TakeBeforeLast('.') : uri.Host;
            return runtime.LoadAssembly(assenblyName);
        }

        internal static bool TryGetAssembly(IRuntime runtime, Uri uri, out Assembly assembly)
        {
            uri.VerifyArgument(nameof(uri)).IsNotNull();

            if (uri.IsAbsoluteUri && uri.IsEmbeddedResource())
            {
                var assenblyName = uri.IsResource() ? uri.Host.TakeBeforeLast('.') : uri.Host;
                assembly = runtime.LoadAssembly(assenblyName);
                return true;
            }
            assembly = null;
            return false;
        }

        // TODO: make this a method to IRuntime
        internal static Stream LoadEmbeddedResource(IRuntime runtime, Assembly asm, string resourceName)
        {
            const string satelliteAssemblySuffix = ".resources";
            var assemblyName = asm.VerifyArgument(nameof(asm)).IsNotNull().Value.GetName().Name.CutEnd(satelliteAssemblySuffix);
            var escapedResourceName = runtime.GetEmbeddedResourcePath(resourceName);
            var manifestResourceName = $"{assemblyName}.{escapedResourceName}";
            /***
             * In case we have a root namespace of the project different from the assembly name,
             * we will attempt to get the root namespace.
             * 
             * The following code will list all namespaces, composed by the 
             * namespaces of all public non-nested types from the assembly.
             * 
             * The namesepaces will be tested sorted by length.
             */
            var stream =
                asm.GetManifestResourceStream(manifestResourceName)
                ?? asm.GetTypes()
                    .Where(type => type.IsPublic && !type.IsNested)
                    .Select(type => type.Namespace ?? string.Empty)
                    .OrderBy(ns => ns.Length)
                    .Select(
                        rootNamespace =>
                        {
                            var mrn = rootNamespace.Length > 0 ? $"{rootNamespace}.{escapedResourceName}" : escapedResourceName;
                            return asm.GetManifestResourceStream(mrn);
                        })
                    .Where(x => x != null)
                    .FirstOrDefault();
            return stream;
        }

        internal bool CanHandle(Uri uri)
        {
            return uri != null && uri.IsEmbeddedResource();
        }

        /// <inheritdoc />
        public Stream GetStream(Uri uri)
        {
            uri.VerifyArgument(nameof(uri))
               .IsNotNull()
               .IsTrue(u => u.IsAbsoluteUri, string.Format("The provided uri `{0}` must be absolute. ", uri))
               .IsTrue(CanHandle, string.Format("The provided uri `{0}` is not a valid embedded resource location. ", uri));

            var runtime = Platform.Runtime;
            var assembly = GetAssembly(runtime, uri);
            var culture = CultureScope.CurrentUICulture;
            var actualAssembly = culture.Equals(CultureInfo.InvariantCulture) ? assembly : runtime.LoadSatelliteAssembly(assembly, culture);
            return actualAssembly == null
                ? null
                : LoadEmbeddedResource(runtime, actualAssembly, uri.PathAndQuery.TakeBeforeFirst('?').Substring(1));
        }
    }
}
#endif