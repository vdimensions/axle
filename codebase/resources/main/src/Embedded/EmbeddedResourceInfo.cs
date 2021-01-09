#if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Axle.Extensions.String;
using Axle.Reflection;
using Axle.Verification;

namespace Axle.Resources.Embedded
{
    /// <summary>
    /// A class representing an embedded resource.
    /// An embedded resource is a file that is included into a managed .dll or .exe by using the <c>Embedded Resource</c>
    /// build action.
    /// </summary>
    public sealed class EmbeddedResourceInfo : ResourceInfo
    {
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
            return resourceName.VerifyArgument(nameof(resourceName)).IsNotNull().Value
                .Replace(" ", "_")
                .Replace("-", "_")
                .Replace("\\", ".")
                .Replace("/", ".");
        }

        internal static bool ContainsEmbeddedResource(Assembly asm, string resourceName)
        {
            const string satelliteAssemblySuffix = ".resources";
            var assemblyName = asm.GetName().Name.TrimEnd(satelliteAssemblySuffix);
            var escapedResourceName = GetEmbeddedResourcePath(resourceName);
            var rootNamespace = assemblyName;
            var manifestResourceName = $"{rootNamespace}.{escapedResourceName}";
            var found = asm.GetManifestResourceNames().Any(x => StringComparer.Ordinal.Equals(manifestResourceName, x));
            if (!found)
            {
                using (var stream = LoadEmbeddedResource(asm, resourceName))
                {
                    return stream != null;
                }
            }
            return true;
        }

        /// <exception cref="FileLoadException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="BadImageFormatException"></exception>
        private static Stream LoadEmbeddedResource(Assembly asm, string resourceName)
        {
            const string satelliteAssemblySuffix = ".resources";
            var assemblyName = asm.GetName().Name.TrimEnd(satelliteAssemblySuffix);
            var escapedResourceName = GetEmbeddedResourcePath(resourceName);
            /***
             * In case we have a root namespace of the project different from the assembly name,
             * we will attempt to get the root namespace.
             * 
             * The following code will list all namespaces, composed by the 
             * namespaces of all public non-nested types from the assembly.
             * 
             * The namespaces will be tested sorted by length.
             */
            var namespaces = new LinkedList<string>(
                asm.GetTypes()
                    .Select(t => new TypeIntrospector(t))
                    .Where(ti => ti.AccessModifier == AccessModifier.Public && !ti.TypeFlags.HasFlag(TypeFlags.Nested))
                    .Select(ti => ti.IntrospectedType.Namespace)
                    .Where(ns => ns != null)
                    .OrderBy(ns => ns.Length));
            namespaces.AddFirst(string.Empty);
            namespaces.AddFirst(assemblyName);
            #if !NETSTANDARD
            var rootNamespace = asm.GetCustomAttributes(false)
            #else
            var rootNamespace = asm.GetCustomAttributes()
            #endif
                .OfType<RootNamespaceAttribute>()
                .Select(x => x.Namespace)
                .SingleOrDefault();
            if (!string.IsNullOrEmpty(rootNamespace))
            {
                namespaces.AddFirst(rootNamespace);
            }
            return namespaces
                .Select(ns => asm.GetManifestResourceStream(ns.Length > 0 ? $"{ns}.{escapedResourceName}" : escapedResourceName))
                .FirstOrDefault(x => x != null);
        }

        private readonly Assembly _assembly;
        private readonly string _actualName;

        internal EmbeddedResourceInfo(Assembly assembly, string name, string actualName, CultureInfo culture) : base(name, culture, "application/octet-stream")
        {
            _assembly = assembly.VerifyArgument(nameof(assembly)).IsNotNull();
            _actualName = actualName.VerifyArgument(nameof(actualName)).IsNotNullOrEmpty();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            try
            {
                return LoadEmbeddedResource(_assembly, _actualName);
            }
            catch (FileNotFoundException e)
            {
                throw new ResourceNotFoundException(Name, Bundle, Culture, e);
            }
            catch (Exception e)
            {
                throw new ResourceLoadException(Name, Bundle, Culture, e);
            }
        }
    }
}
#endif