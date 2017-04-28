using System;
using System.Linq;

using Axle.Verification;


namespace Axle.Reflection
{
    public static partial class MemberExtensions
    {
        public static bool HasAttribute<TAttribute>(this IAttributeTarget member) where TAttribute: Attribute
        {
            return member.VerifyArgument(nameof(member)).IsNotNull().Value.Attributes.Select(x => x.Attribute).OfType<TAttribute>().Any();
        }
    }
}