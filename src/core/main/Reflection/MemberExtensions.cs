using System;
using System.Linq;

using Axle.Verification;


namespace Axle.Reflection
{
    /// <summary>
    /// A static class containing common extension methods to reflected members.
    /// </summary>
    public static partial class MemberExtensions
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
    }
}