#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;
#if NETSTANDARD
using System.Reflection;
#endif

namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments
    /// of type <see cref="System.Type" />.
    /// </summary>
    /// <seealso cref="System.Type"/>
    public static class TypeVerifier
    {
        #if NETFRAMEWORK
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <paramref name="expectedType"/> parameter.
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="System.Type"/>.
        /// </param>
        /// <param name="expectedType">
        /// The type that must be compliant with type of the validated argument.
        /// The compliance check is performed using the <see cref="Type.IsAssignableFrom(Type)"/> method.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argument"/> OR <paramref name="expectedType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentTypeMismatchException">
        /// The argument cannot be assigned to the type specified by the <paramref name="expectedType"/> parameter.
        /// </exception>
        /// <seealso cref="Type.IsAssignableFrom(Type)" />
        #else
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <paramref name="expectedType"/> parameter.
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="Type"/>.
        /// </param>
        /// <param name="expectedType">
        /// The type that must be compliant with type of the validated argument.
        /// The compliance check is performed using the <see cref="TypeInfo.IsAssignableFrom(TypeInfo)" /> method.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argument"/> OR <paramref name="expectedType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentTypeMismatchException">
        /// The argument cannot be assigned to the type specified by the <paramref name="expectedType"/> parameter.
        /// </exception>
        /// <seealso cref="TypeInfo.IsAssignableFrom(TypeInfo)" />
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> Is(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<Type> argument, Type expectedType)
        {
            return IsUnchecked(
                Verifier.IsNotNull(Verifier.VerifyArgument(argument, nameof(argument))).Value,
                Verifier.IsNotNull(Verifier.VerifyArgument(expectedType, nameof(expectedType))));
        }

        #if NETFRAMEWORK
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <typeparamref name="TExpected"/> parameter.
        /// </summary>
        /// <typeparam name="TExpected">
        /// The type that must be compliant with type of the validated argument.
        /// The compliance check is performed using the <see cref="Type.IsAssignableFrom(Type)"/> method.
        /// </typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argument"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentTypeMismatchException">
        /// The argument cannot be assigned to the type specified by the <typeparamref name="TExpected"/> parameter.
        /// </exception>
        /// <seealso cref="Type.IsAssignableFrom(Type)" />
        #else
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <typeparamref name="TExpected"/> parameter.
        /// </summary>
        /// <typeparam name="TExpected">
        /// The type that must be compliant with type of the validated argument.
        /// The compliance check is performed using the <see cref="TypeInfo.IsAssignableFrom(TypeInfo)" /> method.
        /// </typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="Type"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argument"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentTypeMismatchException">
        /// The argument cannot be assigned to the type specified by the <typeparamref name="TExpected"/> parameter.
        /// </exception>
        /// <seealso cref="TypeInfo.IsAssignableFrom(TypeInfo)" />
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> Is<TExpected>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<Type> argument)
        {
            return IsUnchecked(Verifier.IsNotNull(Verifier.VerifyArgument(argument, nameof(argument))).Value, typeof(TExpected));
        }


        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        #if NETSTANDARD
        private static ArgumentReference<Type> IsUnchecked(this ArgumentReference<Type> argument, Type expectedType)
        {
            var actualType = argument.Value;
            if (!expectedType.GetTypeInfo().IsAssignableFrom(actualType.GetTypeInfo()))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, argument.Name);
            }
            return argument;
        }
        #else
        private static ArgumentReference<Type> IsUnchecked(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<Type> argument, Type expectedType)
        {
            var actualType = argument.Value;
            if (!expectedType.IsAssignableFrom(actualType))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, argument.Name);
            }
            return argument;
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Verifies if an argument of type <see cref="Type"/> represents an <see cref="Type.IsAbstract">abstract</see> class.
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <seealso cref="Type.IsAbstract"/>
        #else
        /// <summary>
        /// Verifies if an argument of type <see cref="Type"/> represents an <see cref="TypeInfo.IsAbstract">abstract</see> class.
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <seealso cref="TypeInfo.IsAbstract"/>
        #endif
        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> IsNotAbstract(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<Type> argument)
        {
            var actualType = Verifier.IsNotNull(argument).Value;
            #if NETSTANDARD
            var actualTypeInfo = actualType.GetTypeInfo();
            if (actualTypeInfo.IsInterface || actualTypeInfo.IsAbstract)
            #else
            if (actualType.IsInterface || actualType.IsAbstract)
            #endif
            {
                throw new ArgumentException("The provided type must not be an abstract class or an interface.", argument.Name);
            }
            return argument;
        }
    }
}
#endif