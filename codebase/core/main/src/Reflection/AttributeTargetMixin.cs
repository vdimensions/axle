#if NETSTANDARD || NET20_OR_NEWER
using System;

using Axle.Verification;


namespace Axle.Reflection
{
    // ReSharper disable once UnusedMember.Global
    public static class AttributeTargetMixin
    {
        public static IAttributeInfo[] GetAttributes<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IAttributeTarget attributeTarget) where T: Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(attributeTarget, nameof(attributeTarget)));
            return attributeTarget.GetAttributes(typeof(T));
        }

        public static IAttributeInfo[] GetAttributes<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IAttributeTarget attributeTarget, bool inherit) where T: Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(attributeTarget, nameof(attributeTarget)));
            return attributeTarget.GetAttributes(typeof(T), inherit);
        }

        public static bool IsDefined<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IAttributeTarget attributeTarget, bool inherit) where T : Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(attributeTarget, nameof(attributeTarget)));
            return attributeTarget.IsDefined(typeof(T), inherit);
        }
    }
}
#endif