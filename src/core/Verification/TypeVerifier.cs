using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="System.Type" />.
    /// </summary>
    /// <seealso cref="System.Type"/>
    public static partial class TypeVerifier
    {
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <paramref name="expectedType"/> parameter. 
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="System.Type"/>.
        /// </param>
        /// <param name="expectedType">
        /// The type that must be compliant with type of the validated argument. 
#if !netstandard
        /// The compliance check is performed using the <see cref="Type.IsAssignableFrom(Type)"/> method.
#else
        /// The compliance check is performed using the <see cref="System.Reflection.TypeInfo.IsAssignableFrom(System.Reflection.TypeInfo)" /> method.
#endif
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argument"/> OR <paramref name="expectedType"/> is <c>null</c>.
        /// </exception> 
        /// <exception cref="ArgumentTypeMismatchException">
        /// The argument cannot be assigned the type type specified by the <paramref name="expectedType"/> parameter.
        /// </exception>
#if !netstandard
        /// <seealso cref="Type.IsAssignableFrom(Type)" />
#else
        /// <seealso cref="System.Reflection.TypeInfo.IsAssignableFrom(System.Reflection.TypeInfo)" />
#endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> Is(this ArgumentReference<Type> argument, Type expectedType)
        {
            return IsUnchecked(
                argument.VerifyArgument(nameof(argument)).IsNotNull().Value, 
                expectedType.VerifyArgument(nameof(expectedType)).IsNotNull());
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <typeparamref name="TExpected"/> parameter. 
        /// </summary>
        /// <typeparam name="TExpected">
        /// The type that must be compliant with type of the validated argument. 
#if !netstandard
        /// The compliance check is performed using the <see cref="Type.IsAssignableFrom(Type)"/> method.
#else
        /// The compliance check is performed using the <see cref="System.Reflection.TypeInfo.IsAssignableFrom(System.Reflection.TypeInfo)" /> method.
#endif
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
        /// The argument cannot be assigned the type type specified by the <typeparamref name="TExpected"/> parameter.
        /// </exception>
#if !netstandard
        /// <seealso cref="Type.IsAssignableFrom(Type)" />
#else
        /// <seealso cref="System.Reflection.TypeInfo.IsAssignableFrom(System.Reflection.TypeInfo)" />
#endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> Is<TExpected>(this ArgumentReference<Type> argument)
        {
            return IsUnchecked(
                argument.VerifyArgument(nameof(argument)).IsNotNull().Value, 
                typeof(TExpected));
        }
    }
}
