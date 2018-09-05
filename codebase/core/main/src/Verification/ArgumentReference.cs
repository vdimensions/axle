#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.References;


namespace Axle.Verification
{
    /// <summary>
    /// A <see langword="struct"/> that represents a reference to an argument for a method or constructor. 
    /// The argument reference is usually represented by its name (as defined in the respective method/constructor) and the value passed to it. 
    /// </summary>
    public struct ArgumentReference<T> : IReference<T>
    {
        /// <param name="reference">
        /// The argument reference instance to be unwrapped by this operator. 
        /// </param>
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static implicit operator T(ArgumentReference<T> reference) => reference._value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _name;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        [DebuggerStepThrough]
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal ArgumentReference(string name, T value)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _value = value;
        }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        private ArgumentReference<T> IsOfTypeUnchecked(Type expectedType)
        {
            var actualType = Value.GetType();
            #if NETSTANDARD || NET45_OR_NEWER
            if (!expectedType.GetTypeInfo().IsAssignableFrom(actualType.GetTypeInfo()))
            #else
            if (!expectedType.IsAssignableFrom(actualType))
            #endif
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
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public ArgumentReference<T> IsOfType(Type expectedType)
        {
            return IsOfTypeUnchecked(Verifier.IsNotNull(Verifier.VerifyArgument(expectedType, nameof(expectedType))));
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
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public ArgumentReference<TExpected> IsOfType<TExpected>()
        {
            var arg = IsOfTypeUnchecked(typeof(TExpected));
            object val = arg.Value;
            return new ArgumentReference<TExpected>(arg.Name, (TExpected) val);
        }

        internal string Name => _name;

        /// <summary>
        /// Gets the value passed in the argument the current <see cref="ArgumentReference{T}"/> instance represents.
        /// </summary>
        public T Value
        {
            #if NETSTANDARD || NET45_OR_NEWER
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            #endif
            get => _value;
        }

        /// <inheritdoc cref="IReference{T}.Value"/>
        T IReference<T>.Value => _value;

        /// <inheritdoc cref="IReference.Value"/>
        object IReference.Value => _value;
    }
}
#endif