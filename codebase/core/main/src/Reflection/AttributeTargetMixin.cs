using System;

using Axle.Verification;


namespace Axle.Reflection
{
    // ReSharper disable once UnusedMember.Global
    public static class AttributeTargetMixin
    {
        public static bool IsDefined<T>(this IAttributeTarget attributeTarget, bool inherit) where T : Attribute
        {
            attributeTarget.VerifyArgument(nameof(attributeTarget)).IsNotNull();
            return attributeTarget.IsDefined(typeof(T), inherit);
        }
    }
}