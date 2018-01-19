using System;
using System.Linq;
using System.Reflection;
using Axle.Verification;


namespace Axle.Reflection
{
    partial class MemberExtensions
    {
        public static bool HasAttribute(this IAttributeTarget member, Type attributeType)
        {
            attributeType.VerifyArgument(nameof(attributeType)).IsNotNull().Is<Attribute>();
            return member.VerifyArgument(nameof(member))
                .IsNotNull().Value.Attributes.Where(x => attributeType.GetTypeInfo().IsAssignableFrom(x.Attribute.GetType().GetTypeInfo())).Any();
        }
    }
}