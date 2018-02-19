#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Resources.Extraction.Embedded
{
    /// <summary>
    /// A class representing an embedded resource.
    /// An embedded resource is a file that is included into a managed .dll or .exe by using the <c>Embedded Resource</c>
    /// build action.
    /// </summary>
    public sealed class EmbeddedResourceInfo : ResourceInfo
    {
        private readonly Assembly _assembly;

        /// <summary>
        /// Converts the provided by the <paramref name="resourceName"/> parameter string to
        /// a valid path for an embedded resource.
        /// </summary>
        /// <param name="resourceName">
        /// The resource name to be converted to an embedment path.
        /// </param>
        /// <returns>
        /// A valid embedded resource path.
        /// </returns>
        private static string GetEmbeddedResourcePath(string resourceName)
        {
            return resourceName.VerifyArgument(nameof(resourceName)).IsNotNull().Value.Replace(" ", "_").Replace("-", "_").Replace("\\", ".").Replace("/", ".");
        }

        internal static bool ContainsEmbeddedResource(Assembly asm, string resourceName)
        {
            const string satelliteAssemblySuffix = ".resources";
            var assemblyName = asm.VerifyArgument(nameof(asm)).IsNotNull().Value.GetName().Name.CutEnd(satelliteAssemblySuffix);
            var escapedResourceName = GetEmbeddedResourcePath(resourceName);
            var manifestResourceName = $"{assemblyName}.{escapedResourceName}";
            return asm.GetManifestResourceNames().Any(x => StringComparer.Ordinal.Equals(manifestResourceName, x));
        }

        internal static Stream LoadEmbeddedResource(Assembly asm, string resourceName)
        {
            const string satelliteAssemblySuffix = ".resources";
            var assemblyName = asm.VerifyArgument(nameof(asm)).IsNotNull().Value.GetName().Name.CutEnd(satelliteAssemblySuffix);
            var escapedResourceName = GetEmbeddedResourcePath(resourceName);
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

        internal EmbeddedResourceInfo(Assembly assembly, string name, CultureInfo culture) : base(name, culture, "application/octet-stream")
        {
            _assembly = assembly.VerifyArgument(nameof(assembly)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open() => LoadEmbeddedResource(_assembly, Name);
    }
}
#endif