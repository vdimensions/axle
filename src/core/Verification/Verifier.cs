using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;


namespace Axle.Verification
{
    public static class Verifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> VerifyArgument<T>(this T argument, [Localizable(false)] string argumentName)
        {
            if (argumentName == null)
            {
                throw new ArgumentNullException("argumentName");
            }
            return new ArgumentReference<T>(argumentName, argument);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNull<T>(this ArgumentReference<T> argument, string message)
        {
            if (ReferenceEquals(argument.Value, null))
            {
                throw message == null ? new ArgumentNullException(argument.Name) : new ArgumentNullException(argument.Name, message);
            }
            return argument;
        }
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNull<T>(this ArgumentReference<T> argument)
        {
            return IsNotNull(argument, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNot<T>(this ArgumentReference<T> argument, T value, string message)
        {
            return IsTrue(argument, x => x.Equals(value), message);
        }
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNot<T>(this ArgumentReference<T> argument, T value, IEqualityComparer<T> comparer, string message)
        {
            return IsTrue(argument, x => comparer.Equals(x, value), message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsTrue<T>(this ArgumentReference<T> argument, Predicate<T> condition, string message)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (!condition(argument.Value))
            {
                throw new ArgumentException(argument.Name, message);
            }
            return argument;
        }
    }
}
