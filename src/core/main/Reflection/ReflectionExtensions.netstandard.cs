using System;
using System.Reflection;


namespace Axle.Reflection
{
    partial class ReflectionExtensions
    {
        private static bool IsOverrideUnchecked(MethodInfo mi)
        {
            return mi.GetRuntimeBaseDefinition().DeclaringType != mi.DeclaringType;
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
            var gm = property.GetMethod;
            var sm = property.SetMethod;

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
            var am = eventInfo.AddMethod;
            var rm = eventInfo.RemoveMethod;

            return GetDeclarationTypeUnchecked(am, rm);
        }
    }
}
