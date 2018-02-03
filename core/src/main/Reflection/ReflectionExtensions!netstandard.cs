using System;
using System.Reflection;


namespace Axle.Reflection
{
    partial class ReflectionExtensions
    {
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal static BindingFlags GetFlagsUnsafe(this MethodBase member)
        {
            var result = new BindingFlags();
            if (member.IsStatic)
            {
                result |= BindingFlags.Static;
            }
            if (!member.IsPublic)
            {
                result |= BindingFlags.NonPublic;
            }
            else
            {
                result |= BindingFlags.Public;
            }
            return result;
        }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool IsOverrideUnchecked(MethodInfo mi)
        {
            return mi.GetBaseDefinition().DeclaringType != mi.DeclaringType;
        }

        /// <summary>
        /// Determines the <see cref="DeclarationType"/> of the specified <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/> instance whose declaration type is to be determined.</param>
        /// <returns>
        /// A member of the <see cref="DeclarationType"/> enumeration that corresponds to the specified 
        /// <paramref name="property"/>'s declaration.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="property"/> is <c>null</c></exception>
        /// <seealso cref="DeclarationType"/>
        /// <seealso cref="PropertyInfo"/>
        public static DeclarationType GetDeclarationType(this PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            var gm = property.GetGetMethod(true);
            var sm = property.GetSetMethod(true);

            return GetDeclarationTypeUnchecked(gm, sm);
        }

        /// <summary>
        /// Determines the <see cref="DeclarationType"/> of the specified <paramref name="eventInfo"/>.
        /// </summary>
        /// <param name="eventInfo">
        /// The <see cref="EventInfo"/> instance whose declaration type is to be determined.
        /// </param>
        /// <returns>
        /// A member of the <see cref="DeclarationType"/> enumeration that corresponds to the specified 
        /// <paramref name="eventInfo"/>'s declaration.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="eventInfo"/> is <c>null</c></exception>
        /// <seealso cref="DeclarationType"/>
        /// <seealso cref="EventInfo"/>
        public static DeclarationType GetDeclarationType(this EventInfo eventInfo)
        {
            if (eventInfo == null)
            {
                throw new ArgumentNullException(nameof(eventInfo));
            }
            var am = eventInfo.GetAddMethod(true);
            var rm = eventInfo.GetRemoveMethod(true);

            return GetDeclarationTypeUnchecked(am, rm);
        }
    }
}
