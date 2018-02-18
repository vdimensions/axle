#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using Axle.Environment;
using Axle.Extensions.String;
using Axle.Extensions.Uri;
using Axle.Verification;


namespace Axle.Resources.Extraction.Streaming
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
                    .FirstOrDefault(x => x != null);
            return stream;
        }

        internal static bool CheckEmbeddedResourceName(IRuntime runtime, Assembly asm, string resourceName)
        {
            const string satelliteAssemblySuffix = ".resources";
            var assemblyName = asm.VerifyArgument(nameof(asm)).IsNotNull().Value.GetName().Name.CutEnd(satelliteAssemblySuffix);
            var escapedResourceName = runtime.GetEmbeddedResourcePath(resourceName);
            var manifestResourceName = $"{assemblyName}.{escapedResourceName}";
            return asm.GetManifestResourceNames().Any(x => StringComparer.Ordinal.Equals(manifestResourceName, x));
        }

        public static bool TryCreate(Uri uri, CultureInfo culture, string name, out EmbeddedResourceUriStreamAdapter adapter)
        {
            var runtime = Platform.Runtime;
            var assembly = GetAssembly(runtime, uri);
            var actualAssembly = culture.Equals(CultureInfo.InvariantCulture) ? assembly : runtime.LoadSatelliteAssembly(assembly, culture);
            /*
             * Only create adapter if there is a satellite assembly when the culture is not invariant.
             * Also, never create an adapter if the assembly does not contain the requested resource.
             */
            if (actualAssembly != null && CheckEmbeddedResourceName(runtime, actualAssembly, name))
            {
                adapter = new EmbeddedResourceUriStreamAdapter(actualAssembly, runtime);
                return true;
            }
            adapter = null;
            return false;
        }

        private readonly Assembly _assembly;
        private readonly IRuntime _runtime;

        private EmbeddedResourceUriStreamAdapter(Assembly assembly, IRuntime runtime)
        {
            _assembly = assembly;
            _runtime = runtime;
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

            return LoadEmbeddedResource(_runtime, _assembly, uri.PathAndQuery.TakeBeforeFirst('?').Substring(1));
        }
    }
}
#endif