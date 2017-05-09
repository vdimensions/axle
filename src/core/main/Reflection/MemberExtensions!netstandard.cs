using System;
using System.Linq;

using Axle.Verification;


namespace Axle.Reflection
{
    partial class MemberExtensions
    {
        public static bool HasAttribute(this IAttributeTarget member, Type attributeType)
        {
            attributeType.VerifyArgument(nameof(attributeType)).IsNotNull().Is<Attribute>();
            return member.VerifyArgument(nameof(member)).IsNotNull().Value.Attributes.Where(x => attributeType.IsAssignableFrom(x.Attribute.GetType())).Any();
        }
    }
}