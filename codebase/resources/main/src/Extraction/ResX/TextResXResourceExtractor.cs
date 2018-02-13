#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System;
using System.Globalization;

using Axle.Extensions.Uri;
using Axle.Verification;


namespace Axle.Resources.Extraction.ResX
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting only text resources.
    /// </summary>
    public sealed class TextResXResourceExtractor : IResourceExtractor
    {
        private readonly Type _type;
        private readonly Uri _baseUri;

        /// <summary>
        /// Creates a new instance of the <see cref="TextResXResourceExtractor"/> class.
        /// </summary>
        /// <param name="type">
        /// The type that represents the .NET resource container.
        /// </param>
        public TextResXResourceExtractor(Type type)
        {
            _type = type.VerifyArgument(nameof(type)).IsNotNull();
        }

        public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
        {
            location = location.Resolve($"./{name}");
            var val = new System.Resources.ResourceManager(_type).GetString(name, culture);
            if (val == null)
            {
                resource = null;
                return false;
            }
            resource = new TextResourceInfo(location, name, culture, val);
            return true;
        }
    }
}
#endif