#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;
using System.Reflection;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public class EmbeddedResourceExtractor : IResourceExtractor
    {
        private readonly Assembly _assembly;
    
        public EmbeddedResourceExtractor(Assembly assembly)
        {
            _assembly = assembly.VerifyArgument(nameof(assembly)).IsNotNull();
        }

        public ResourceInfo Extract(string name, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
#endif