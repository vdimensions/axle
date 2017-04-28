using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Verification
{
    /// <summary>
    /// A struct that represents a reference to an argument for a method or constructor. 
    /// The argument reference is usually represented by its name (as defined in the respective method/constructor) and the value passed to it. 
    /// </summary>
    public partial struct ArgumentReference<T>
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        private ArgumentReference<T> IsOfTypeUnchecked(Type expectedType)
        {
            var actualType = Value.GetType();
            if (!expectedType.GetTypeInfo().IsAssignableFrom(actualType.GetTypeInfo()))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, Name);
            }
            return this;
        }

        /// <summary>
        /// Determines whether the argument represented by this <see cref="ArgumentReference{T}"/> instance is of the type specified by 
        /// the <paramref name="expectedType"/> parameter.
        /// </summary>
        /// <param name="expectedType">
        /// The expected type for the argument represented by the current <see cref="ArgumentReference{T}"/> instance. 
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public ArgumentReference<T> IsOfType(Type expectedType)
        {
            return IsOfTypeUnchecked(expectedType.VerifyArgument("expectedType").IsNotNull());
        }

        /// <summary>
        /// Determines whether the argument represented by this <see cref="ArgumentReference{T}"/> instance is of the type specified by 
        /// the <typeparamref name="TExpected"/> generic parameter.
        /// </summary>
        /// <typeparam name="TExpected">
        /// The expected type for the argument represented by the current <see cref="ArgumentReference{T}"/> instance. 
        /// </typeparam>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public ArgumentReference<TExpected> IsOfType<TExpected>()
        {
            var arg = IsOfTypeUnchecked(typeof(TExpected));
            object val = arg.Value;
            return new ArgumentReference<TExpected>(arg.Name, (TExpected) val);
        }
    }
}