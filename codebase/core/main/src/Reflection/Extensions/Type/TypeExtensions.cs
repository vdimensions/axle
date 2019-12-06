#if NETSTANDARD || NET20_OR_NEWER
using System;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif
using Axle.Verification;


namespace Axle.Reflection.Extensions.Type
{
    using Type = System.Type;

    /// <summary>
    /// A <see langword="static"/> class containing extension methods for instances of the 
    /// <see cref="System.Type"/> type.
    /// </summary>
    public static class TypeExtensions
    {
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Gets the underlying <see cref="TypeCode">type code</see> of the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type">type</see> whose underlying <see cref="TypeCode">type code</see> 
        /// to get.</param>
        /// <returns>
        /// The <see cref="TypeCode"/> value of the underlying type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> is <c><see langword="null"/></c>.
        /// </exception>
        public static TypeCode GetTypeCode(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type) => 
            Type.GetTypeCode(Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type))).Value);
        #endif

        /// <summary>
        /// Determines whether the <paramref name="type"/> type can be assigned to an instance of a specified type.
        /// </summary>
        /// <param name="type">
        /// The current <see cref="Type">type</see> to check.
        /// </param>
        /// <param name="other">
        /// The suspected base type to check.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if any of the following conditions is met: 
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type and the specified one represent the same type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is derived either directly or indirectly from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is derived directly from the specified one if it inherits from the 
        /// specified type; 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is derived indirectly from the specified type if it inherits from a 
        /// succession of one or more classes that inherit from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The specified type is an interface that the <paramref name="type"/> type implements. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is a generic type parameter, and the specified type represents one of 
        /// its constraints. 
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="type"/> or <paramref name="other"/> is <c><see langword="null"/></c>.
        /// </exception>
        public static bool ExtendsOrImplements(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type, 
                Type other)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            Verifier.IsNotNull(Verifier.VerifyArgument(other, nameof(other)));
            #if NETSTANDARD || NET45_OR_NEWER
            return other.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
            #else
            return other.IsAssignableFrom(type);
            #endif
        }

        /// <summary>
        /// Determines whether the <paramref name="type"/> type can be assigned to an instance of a specified type.
        /// </summary>
        /// <param name="type">
        /// The current <see cref="Type">type</see> to check.
        /// </param>
        /// <typeparam name="T">
        /// The suspected base type to check.
        /// </typeparam>
        /// <returns>
        /// <c><see langword="true"/></c> if any of the following conditions is met: 
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type and the specified one represent the same type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is derived either directly or indirectly from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is derived directly from the specified one if it inherits from the 
        /// specified type; 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is derived indirectly from the specified type if it inherits from a 
        /// succession of one or more classes that inherit from the specified type. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The specified type is an interface that the <paramref name="type"/> type implements. 
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="type"/> type is a generic type parameter, and the specified type represents one of 
        /// its constraints. 
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> is <c><see langword="null"/></c>.
        /// </exception>
        public static bool ExtendsOrImplements<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            #if NETSTANDARD || NET45_OR_NEWER
            return typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
            #else
            return typeof(T).IsAssignableFrom(type);
            #endif
        }

        /// <summary>
        /// Determines if a specific <paramref name="type"/> is a superclass of a given <paramref name="other"/> type.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type">type</see> to check.
        /// </param>
        /// <param name="other">
        /// The potential subclassing type to check.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <paramref name="type"/> is higher in the inheritence hierarchy
        /// of the provided <paramref name="other"/> type;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public static bool IsBaseOf(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type, 
                Type other)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            Verifier.IsNotNull(Verifier.VerifyArgument(other, nameof(other)));
            #if NETSTANDARD || NET45_OR_NEWER
            return type.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
            #else
            return type.IsAssignableFrom(other);
            #endif
        }

        /// <summary>
        /// Determines if a specific <paramref name="type"/> is a superclass of a given <paramref name="other"/> type.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type">type</see> to check.
        /// </param>
        /// <typeparam name="T">
        /// The potential subclassing type to check.
        /// </typeparam>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <paramref name="type"/> is higher in the inheritence hierarchy
        /// of the provided  type <typeparamref name="T"/>;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        public static bool IsBaseOf<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            #if NETSTANDARD || NET45_OR_NEWER
            return type.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo());
            #else
            return type.IsAssignableFrom(typeof(T));
            #endif
        }

        /// <summary>
        /// Determines if the provided <paramref name="type"/> is a delegate.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type">type</see> to check if represents a delegate.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the provided <paramref name="type"/> is a delegate;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> is <c><see langword="null"/></c>.
        /// </exception>
        public static bool IsDelegate(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            return typeof(MulticastDelegate)
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                .IsAssignableFrom(type.GetTypeInfo().BaseType.GetTypeInfo())
                #else
                .IsAssignableFrom(type.BaseType)
                #endif
                ;
        }

        #if NETSTANDARD || NET35_OR_NEWER
        /// <summary>
        /// Checks whether the specified <paramref name="type"/> is a nullable type.
        /// </summary>
        /// <param name="type">
        /// The type to check if nullable.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <paramref name="type"/> is a nullable type; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> is <c><see langword="null"/></c>.
        /// </exception>
        public static bool IsNullableType(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                Type type)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            #if NETSTANDARD || NET45_OR_NEWER
            var ti = type.GetTypeInfo();
            return ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(Nullable<>);
            #else
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
            #endif
        }
        #endif
    }
}
#endif