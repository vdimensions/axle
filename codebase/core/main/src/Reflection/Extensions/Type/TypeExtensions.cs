#if NETSTANDARD || NET35_OR_NEWER
using System;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Reflection.Extensions.Type
{
    using Type = System.Type;

    /// <summary>
    /// A static class containing extension methods for instances of the <see cref="System.Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK

        /// <summary>
        /// Gets the underlying <see cref="TypeCode">type code</see> of the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type">type</see> whose underlying <see cref="TypeCode">type code</see> to get.</param>
        /// <returns>
        /// The <see cref="TypeCode"/> value of the underlying type.
        /// </returns>
        public static TypeCode GetTypeCode(this Type type) => Type.GetTypeCode(type.VerifyArgument(nameof(type)).IsNotNull().Value);
        #endif

        /// <summary>
        /// Determines whether the <paramref name="current"/> type can be assigned to an instance of a specified type.
        /// </summary>
        /// <param name="current">
        /// The current type to check.
        /// </param>
        /// <param name="other">
        /// The suspected base type to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if any of the following conditions is met: 
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type and the specified one represent the same type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is derived either directly or indirectly from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is derived directly from the specified one if it inherits from the specified type; 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is derived indirectly from the specified type if it inherits from a succession of one or more classes that inherit from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The specified type is an interface that the <paramref name="current"/> type implements. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is a generic type parameter, and the specified type represents one of its constraints. 
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public static bool ExtendsOrImplements(this Type current, Type other)
        {
            current.VerifyArgument(nameof(current)).IsNotNull();
            other.VerifyArgument(nameof(other)).IsNotNull();
            #if NETSTANDARD || NET45_OR_NEWER
            return other.GetTypeInfo().IsAssignableFrom(current.GetTypeInfo());
            #else
            return other.IsAssignableFrom(current);
            #endif
        }

        /// <summary>
        /// Determines whether the <paramref name="current"/> type can be assigned to an instance of a specified type.
        /// </summary>
        /// <param name="current">
        /// The current type to check.
        /// </param>
        /// <typeparam name="T">
        /// The suspected base type to check.
        /// </typeparam>
        /// <returns>
        /// <c>true</c> if any of the following conditions is met: 
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type and the specified one represent the same type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is derived either directly or indirectly from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is derived directly from the specified one if it inherits from the specified type; 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is derived indirectly from the specified type if it inherits from a succession of one or more classes that inherit from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The specified type is an interface that the <paramref name="current"/> type implements. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="current"/> type is a generic type parameter, and the specified type represents one of its constraints. 
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public static bool ExtendsOrImplements<T>(this Type current)
        {
            current.VerifyArgument(nameof(current)).IsNotNull();
            #if NETSTANDARD || NET45_OR_NEWER
            return typeof(T).GetTypeInfo().IsAssignableFrom(current.GetTypeInfo());
            #else
            return typeof(T).IsAssignableFrom(current);
            #endif
        }

        public static bool IsBaseOf(this Type type, Type other)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            other.VerifyArgument(nameof(other)).IsNotNull();
            #if NETSTANDARD || NET45_OR_NEWER
            return type.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
            #else
            return type.IsAssignableFrom(other);
            #endif
        }
        public static bool IsBaseOf<T>(this Type type)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            #if NETSTANDARD || NET45_OR_NEWER
            return type.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo());
            #else
            return type.IsAssignableFrom(typeof(T));
            #endif
        }

        /// <summary>
        /// Checks whether the specified <paramref name="type"/> is a nullable type.
        /// </summary>
        /// <param name="type">
        /// The type to check if nullable.
        /// </param>
        /// <returns>
        /// <c>true</c> if the current <paramref name="type"/> is a nullable type; <c>false</c> otherwise.
        /// </returns>
        public static bool IsNullableType(this Type type)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            #if NETSTANDARD || NET45_OR_NEWER
            var ti = type.GetTypeInfo();
            return ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(Nullable<>);
            #else
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
            #endif
        }
    }
}
#endif