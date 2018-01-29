#if !NETSTANDARD
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


namespace Axle.Resources
{
    internal sealed class EmbeddedResourceUriStreamAdapter : IUriStreamAdapter
    {
        public bool CanHandle(Uri uri)
        {
            return uri != null && uri.IsEmbeddedResource();
        }

        public Stream GetStream(Uri uri)
        {
            uri.VerifyArgument(nameof(uri))
               .IsNotNull()
               .IsTrue(u => u.IsAbsoluteUri, string.Format("The provided uri `{0}` must be absolute. ", uri))
               .IsTrue(CanHandle, string.Format("The provided uri `{0}` is not a valid embedded resource location. ", uri));

            var assembly = uri.GetAssembly();
            var runtime = Platform.Runtime;
            var culture = CultureScope.CurrentUICulture;
            var actualAssembly = culture.Equals(CultureInfo.InvariantCulture) ? assembly : runtime.LoadSatelliteAssembly(assembly, culture);
            return actualAssembly == null
                ? null
                : LoadEmbeddedResource(runtime, actualAssembly, uri.PathAndQuery.Substring(1).TakeBeforeFirst('?'));
        }

        // TODO: make this a method to IRuntime
        public static Stream LoadEmbeddedResource(IRuntime runtime, Assembly asm, string resourceName)
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
    }
}
#endif