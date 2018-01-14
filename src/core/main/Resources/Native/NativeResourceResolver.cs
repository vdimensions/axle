using System;
using System.Globalization;

using Axle.Verification;


namespace Axle.Resources.Native
{
    internal sealed class NativeResourceResolver
    {
        private readonly Type _resourceType;

        public NativeResourceResolver(Type resourceType) 
        {
            _resourceType = resourceType.VerifyArgument(nameof(resourceType)).IsNotNull();
        }

        public object Resolve(string key, CultureInfo culture)
        {
            var resourceSet = new System.Resources.ResourceManager(_resourceType).GetResourceSet(culture, true, false);
            if (resourceSet == null)
            {
                return null;
            }
            using (resourceSet)
            {
                return resourceSet.GetObject(key);
            }
        }
    }
}