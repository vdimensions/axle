#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System;
using System.Globalization;

using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Resources.Extraction.ResX
{
    /// <summary>
    /// An class to serve as a base for <see cref="IResourceExtractor"/> implementations
    /// that handle the native .NET resource objects.
    /// </summary>
    /// <seealso cref="IResourceExtractor "/>
    public abstract class AbstractResXResourceExtractor : AbstractResourceExtractor
    {
        private const string UriSchemeResX = "resx";

        /// <summary>
        /// Creates a new instance of the current <see cref="AbstractResXResourceExtractor"/> implementation.
        /// </summary>
        protected AbstractResXResourceExtractor() : base(ResourceContextSplitStrategy.ByLocationThenCulture) { }
        protected AbstractResXResourceExtractor(ResourceContextSplitStrategy splitrStrategy) : base(splitrStrategy) { }

        private bool TryGetResXType(Uri location, out Type type, out string prefix)
        {
            type = null;
            prefix = string.Empty;
            if (!location.IsAbsoluteUri || !location.Scheme.Equals(UriSchemeResX, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            var assembly = Axle.Environment.Platform.Runtime.LoadAssembly(location.Host);
            var pq = location.PathAndQuery.TrimStart('/');
            try
            {
                type = assembly?.GetType(pq.TakeBeforeFirst('/'));
                prefix = pq.TakeAfterFirst('/');
                return type != null;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        protected sealed override ResourceInfo Extract(Uri location, CultureInfo culture, string name)
        {
            if (TryGetResXType(location, out var type, out var prefix))
            {
                return ExtractResource(new Uri($"{prefix}/", UriKind.Relative), culture, type, name.VerifyArgument(nameof(name)).IsNotNullOrEmpty());
            }
            return null;
        }

        protected abstract ResourceInfo ExtractResource(Uri location, CultureInfo culture, Type resxType, string name);
    }
}
#endif