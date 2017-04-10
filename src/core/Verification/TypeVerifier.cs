using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="System.Type" />.
    /// </summary>
    /// <seealso cref="System.Type"/>
    public static class TypeVerifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        private static ArgumentReference<Type> IsUnchecked(this ArgumentReference<Type> argument, Type expectedType)
        {
            var actualType = argument.Value;
            if (!expectedType.IsAssignableFrom(actualType))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, argument.Name);
            }
            return argument;
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{Type}">argument reference</see> represented by the <paramref name="argument"/>
        /// can be assigned to the type specified by the <paramref name="expectedType"/> parameter. 
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{Type}"/> that represents a method/constructor argument of type <see cref="System.Type"/>.
        /// </param>
        /// <param name="expectedType">
        /// The type that must be compliant with type of the validated argument. 
        /// The compliance check is performed using the <see cref="Type.IsAssignableFrom"/> method.
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
        /// <seealso cref="Type.IsAssignableFrom" />
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        /// The compliance check is performed using the <see cref="Type.IsAssignableFrom"/> method.
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
        /// <seealso cref="Type.IsAssignableFrom" />
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
