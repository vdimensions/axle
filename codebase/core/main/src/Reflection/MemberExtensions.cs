using System;
using System.Linq;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Reflection
{
    /// <summary>
    /// A static class containing common extension methods to reflected members.
    /// </summary>
    public static class MemberExtensions
    {
        /// <summary>
        /// Determines if the given <see cref="IAttributeTarget"/> instance has an attribute of the type <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// The type of an attribute to check if defined for the current <see cref="IAttributeTarget"/> instance. 
        /// <para>
        /// Only types inheriting from the <see cref="Attribute"/> class must be specified.
        /// </para>
        /// </typeparam>
        /// <param name="member">
        /// The target <see cref="IAttributeTarget"/> instance this extension method is executed against.
        /// </param>
        /// <returns>
        /// <c>true</c> if the given <see cref="IAttributeTarget"/> instance has an attribute of the type <typeparamref name="TAttribute"/> defined; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="member"/> is <c>null</c>.
        /// </exception>
        public static bool HasAttribute<TAttribute>(this IAttributeTarget member) where TAttribute: Attribute
        {
            return member.VerifyArgument(nameof(member)).IsNotNull().Value.Attributes.Select(x => x.Attribute).OfType<TAttribute>().Any();
        }

        /// <summary>
        /// Determines if the given <see cref="IAttributeTarget"/> instance has an attribute of the type <paramref name="attributeType"/> defined.
        /// </summary>
        /// <param name="member">
        /// The target <see cref="IAttributeTarget"/> instance this extension method is executed against.
        /// </param>
        /// <param name="attributeType">
        /// The type of an attribute to check if defined for the current <see cref="IAttributeTarget"/> instance. 
        /// <para>
        /// Only types inheriting from the <see cref="Attribute"/> class must be specified.
        /// </para>
        /// </param>
        /// <returns>
        /// <c>true</c> if the given <see cref="IAttributeTarget"/> instance has an attribute of the type <paramref name="attributeType"/> defined; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="member"/> or <paramref name="attributeType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentTypeMismatchException">
        /// The <paramref name="attributeType"/> does not represent a valid <see cref="Attribute"/> type.
        /// </exception>
        public static bool HasAttribute(this IAttributeTarget member, Type attributeType)
        {
            attributeType.VerifyArgument(nameof(attributeType)).IsNotNull().Is<Attribute>();
            return member.VerifyArgument(nameof(member)).IsNotNull().Value.Attributes
                #if NETSTANDARD || NET45_OR_NEWER
                .Any(x => attributeType.GetTypeInfo().IsAssignableFrom(x.Attribute.GetType().GetTypeInfo()));
                #else
                .Any(x => attributeType.IsAssignableFrom(x.Attribute.GetType()));
                #endif
        }
    }
}