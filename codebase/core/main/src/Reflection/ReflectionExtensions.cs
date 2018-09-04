using System;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    /// <summary>
    /// A static class that contains extension methods to aid the .NET's reflection API.
    /// </summary>
    public static class ReflectionExtensions
    {
        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal static DeclarationType GetDeclarationType(bool isStatic, bool isAbstract, bool isVirtual, bool isOverride, bool isHideBySig, bool isSealed)
        {
            var declarationType = DeclarationType.None;
            if (isStatic)
            {
                declarationType |= DeclarationType.Static;
            }
            else
            {
                declarationType |= DeclarationType.Instance;
                if (isAbstract)
                {
                    declarationType |= DeclarationType.Abstract;
                }
                else if (isVirtual)
                {
                    declarationType |= DeclarationType.Virtual;
                }
                else if (isOverride && !isHideBySig)
                {
                    declarationType |= DeclarationType.Override;
                }
            }
            if (isHideBySig)
            {
                declarationType |= DeclarationType.HideBySig;
            }
            if (isSealed)
            {
                declarationType |= DeclarationType.Sealed;
            }
            return declarationType;
        }

        /// <summary>
        /// Determines if a specified <paramref name="method"/> overrides a corresponding method from a base class.
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo" /> to check for override.</param>
        /// <returns>
        /// <c>true</c> if the specified <paramref name="method"/> overrides a corresponding method from a base class; <c>false</c> otherwise
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is <c>null</c></exception>
        public static bool IsOverride(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            return IsOverrideUnchecked(method);
        }

        /// <summary>
        /// Determines if a specified <paramref name="method"/> overrides a corresponding method from a base class.
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo" /> to check for override.</param>
        /// <returns>
        /// <c>true</c> if the specified <paramref name="method"/> overrides a corresponding method from a base class; <c>false</c> otherwise.
        /// This method also returns <c>false</c> if the <paramref name="method"/> parameter is a constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is <c>null</c></exception>
        public static bool IsOverride(this MethodBase method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            return method is MethodInfo mi && IsOverrideUnchecked(mi);
        }

        internal static DeclarationType GetDeclarationTypeUnchecked(MethodInfo gm, MethodInfo sm)
        {
            var isStatic = (gm ?? sm).IsStatic;
            var isAbstract = (gm != null && gm.IsAbstract) || (sm != null && sm.IsAbstract);
            var isVirtual = (gm != null && gm.IsVirtual) || (sm != null && sm.IsVirtual);
            var isOverride = (gm != null && gm.IsOverride()) || (sm != null && sm.IsOverride());
            var isHideBySig = (gm != null && gm.IsHideBySig) || (sm != null && sm.IsHideBySig);
            var isSealed = (gm != null && gm.IsFinal) || (sm != null && sm.IsFinal);
            return GetDeclarationType(
                isStatic,
                isAbstract,
                isVirtual,
                isOverride,
                isHideBySig,
                isSealed);
        }

        /// <summary>
        /// Determines the <see cref="DeclarationType"/> of the specified <paramref name="methodBase"/>.
        /// </summary>
        /// <param name="methodBase">
        /// The <see cref="MethodBase"/> instance whose declaration type is to be determined.
        /// </param>
        /// <returns>
        /// A member of the <see cref="DeclarationType"/> enumeration that corresponds to the specified 
        /// <paramref name="methodBase"/>'s declaration.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="methodBase"/> is <c>null</c></exception>
        /// <seealso cref="DeclarationType"/>
        /// <seealso cref="MethodBase"/>
        public static DeclarationType GetDeclarationType(this MethodBase methodBase)
        {
            if (methodBase == null)
            {
                throw new ArgumentNullException(nameof(methodBase));
            }
            var isNotConstructor = !methodBase.IsConstructor;
            return GetDeclarationType(
                methodBase.IsStatic,
                isNotConstructor && methodBase.IsAbstract,
                isNotConstructor && methodBase.IsVirtual,
                isNotConstructor && methodBase.IsOverride(),
                isNotConstructor && methodBase.IsHideBySig,
                methodBase.IsFinal);
        }

        /// <summary>
        /// Determines the <see cref="DeclarationType"/> of the specified <paramref name="field"/>.
        /// </summary>
        /// <param name="field">
        /// The <see cref="FieldInfo"/> instance whose declaration type is to be determined.
        /// </param>
        /// <returns>
        /// A member of the <see cref="DeclarationType"/> enumeration that corresponds to the specified 
        /// <paramref name="field"/>'s declaration.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="field"/> is <c>null</c></exception>
        /// <seealso cref="DeclarationType"/>
        /// <seealso cref="FieldInfo"/>
        public static DeclarationType GetDeclarationType(this FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            return GetDeclarationType(
                field.IsStatic,
                false,
                false,
                false,
                false,
                field.IsLiteral);
        }

        /// <summary>
        /// Determines the <see cref="DeclarationType"/> of the specified <paramref name="member"/>.
        /// </summary>
        /// <param name="member">
        /// The <see cref="MemberInfo"/> instance whose declaration type is to be determined.
        /// </param>
        /// <returns>
        /// A member of the <see cref="DeclarationType"/> enumeration that corresponds to the specified 
        /// <paramref name="member"/>'s declaration.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="member"/> is <c>null</c></exception>
        /// <seealso cref="DeclarationType"/>
        /// <seealso cref="MemberInfo"/>
        public static DeclarationType GetDeclarationType(this MemberInfo member)
        {
            switch (member)
            {
                case null:
                    throw new ArgumentNullException(nameof(member));
                case MethodBase method:
                    return GetDeclarationType(method);
                case PropertyInfo property:
                    return GetDeclarationType(property);
                case FieldInfo field:
                    return GetDeclarationType(field);
                case EventInfo evt:
                    return GetDeclarationType(evt);
                default:
                    throw new ArgumentException("Cannot determine member's declaration type", nameof(member));
            }
        }

        private static bool DeclarationTypeFlagCompare(DeclarationType all, DeclarationType flag)
        {
            return (all & flag) == flag;
        }

        /// <summary>
        /// Determines if a <see cref="DeclarationType"/> value contains the <see cref="DeclarationType.Instance"/> flag.
        /// </summary>
        /// <param name="declaration">The <see cref="DeclarationType"/> value to check.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="declaration"/> value contains the <see cref="DeclarationType.Instance"/> flag;
        /// <c>false</c> otherwise
        /// </returns>
        /// <seealso cref="DeclarationType.Instance"/>
        /// <seealso cref="DeclarationType"/>
        public static bool IsInstance(this DeclarationType declaration) => DeclarationTypeFlagCompare(declaration, DeclarationType.Instance);

        /// <summary>
        /// Determines if a <see cref="DeclarationType"/> value contains the <see cref="DeclarationType.Static"/> flag.
        /// </summary>
        /// <param name="declaration">The <see cref="DeclarationType"/> value to check.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="declaration"/> value contains the <see cref="DeclarationType.Static"/> flag;
        /// <c>false</c> otherwise
        /// </returns>
        /// <seealso cref="DeclarationType.Static"/>
        /// <seealso cref="DeclarationType"/>
        public static bool IsStatic(this DeclarationType declaration) => DeclarationTypeFlagCompare(declaration, DeclarationType.Static);

        /// <summary>
        /// Determines if a <see cref="DeclarationType"/> value contains the <see cref="DeclarationType.Abstract"/> flag.
        /// </summary>
        /// <param name="declaration">The <see cref="DeclarationType"/> value to check.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="declaration"/> value contains the <see cref="DeclarationType.Abstract"/> flag;
        /// <c>false</c> otherwise
        /// </returns>
        /// <seealso cref="DeclarationType.Abstract"/>
        /// <seealso cref="DeclarationType"/>
        public static bool IsAbstract(this DeclarationType declaration) => DeclarationTypeFlagCompare(declaration, DeclarationType.Abstract);

        /// <summary>
        /// Determines if a <see cref="DeclarationType"/> value contains the <see cref="DeclarationType.Override"/> flag.
        /// </summary>
        /// <param name="declaration">The <see cref="DeclarationType"/> value to check.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="declaration"/> value contains the <see cref="DeclarationType.Override"/> flag;
        /// <c>false</c> otherwise
        /// </returns>
        /// <seealso cref="DeclarationType.Override"/>
        /// <seealso cref="DeclarationType"/>
        public static bool IsOverride(this DeclarationType declaration) => DeclarationTypeFlagCompare(declaration, DeclarationType.Override);

        /// <summary>
        /// Determines if a <see cref="DeclarationType"/> value contains the <see cref="DeclarationType.HideBySig"/> flag.
        /// </summary>
        /// <param name="declaration">The <see cref="DeclarationType"/> value to check.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="declaration"/> value contains the <see cref="DeclarationType.HideBySig"/> flag;
        /// <c>false</c> otherwise
        /// </returns>
        /// <seealso cref="DeclarationType.HideBySig"/>
        /// <seealso cref="DeclarationType"/>
        public static bool IsHideBySig(this DeclarationType declaration) => DeclarationTypeFlagCompare(declaration, DeclarationType.HideBySig);

        /// <summary>
        /// Determines if a <see cref="DeclarationType"/> value contains the <see cref="DeclarationType.Sealed"/> flag.
        /// </summary>
        /// <param name="declaration">The <see cref="DeclarationType"/> value to check.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="declaration"/> value contains the <see cref="DeclarationType.Sealed"/> flag;
        /// <c>false</c> otherwise
        /// </returns>
        /// <seealso cref="DeclarationType.Sealed"/>
        /// <seealso cref="DeclarationType"/>
        public static bool IsSealed(this DeclarationType declaration) => DeclarationTypeFlagCompare(declaration, DeclarationType.Sealed);

        public static object InvokeStatic(this IMethod @this, params object[] args)
        {
            @this.VerifyArgument(nameof(@this)).IsNotNull();
            if ((@this.Declaration & DeclarationType.Static) != DeclarationType.Static)
            {
                throw new InvalidOperationException(string.Format("Cannot invoke instance method {0} as a static method.", @this.Name));
            }
            return @this.Invoke(null, args);
        }

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        #if NETSTANDARD
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
        #endif

        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static bool IsOverrideUnchecked(MethodInfo mi) => mi.GetRuntimeBaseDefinition().DeclaringType != mi.DeclaringType;
        #else
        private static bool IsOverrideUnchecked(MethodInfo mi) => mi.GetBaseDefinition().DeclaringType != mi.DeclaringType;
        #endif

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
            #if NETSTANDARD
            var gm = property.GetMethod;
            var sm = property.SetMethod;
            #else
            var gm = property.GetGetMethod(true);
            var sm = property.GetSetMethod(true);
            #endif
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
            #if NETSTANDARD
            var am = eventInfo.AddMethod;
            var rm = eventInfo.RemoveMethod;
            #else
            var am = eventInfo.GetAddMethod(true);
            var rm = eventInfo.GetRemoveMethod(true);
            #endif
            return GetDeclarationTypeUnchecked(am, rm);
        }
    }
}
