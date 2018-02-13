#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System;
using System.Globalization;

using Axle.Verification;


namespace Axle.Resources.Extraction.ResX
{
    /// <summary>
    /// The .NET's native resource extractor implementation, supporting only text resources.
    /// </summary>
    public sealed class TextResXResourceExtractor : AbstractResourceExtractor
    {
        private readonly Type _type;

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

        protected override ResourceInfo Extract(Uri location, CultureInfo culture, string name)
        {
            var val = new System.Resources.ResourceManager(_type).GetString(name, culture);
            return val == null ? null : new TextResourceInfo(name, culture, val);
        }
    }
}
#endif