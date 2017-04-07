using System;
using System.Diagnostics;


namespace Axle.Verification
{
    public static class ComparableVerifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThan<T>(this ArgumentReference<T> argument, T minValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(minValue, null))
            {
                throw new ArgumentNullException("minValue");
            }
            if (argument.Value.CompareTo(minValue) > 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThan<T>(this ArgumentReference<T> argument, T minValue) where T: IComparable<T>
        {
            return IsGreaterThan(argument, minValue, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThanOrEqualTo<T>(this ArgumentReference<T> argument, T minValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(minValue, null))
            {
                throw new ArgumentNullException("minValue");
            }
            if (argument.Value.CompareTo(minValue) >= 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThanOrEqualTo<T>(this ArgumentReference<T> argument, T minValue) where T: IComparable<T>
        {
            return IsGreaterThanOrEqualTo(argument, minValue, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThan<T>(this ArgumentReference<T> argument, T maxValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(maxValue, null))
            {
                throw new ArgumentNullException("maxValue");
            }
            if (argument.Value.CompareTo(maxValue) < 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThan<T>(this ArgumentReference<T> argument, T maxValue) where T: IComparable<T>
        {
            return IsLessThan(argument, maxValue, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThanOrEqualTo<T>(this ArgumentReference<T> argument, T maxValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(maxValue, null))
            {
                throw new ArgumentNullException("maxValue");
            }
            if (argument.Value.CompareTo(maxValue) <- 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThanOrEqualTo<T>(this ArgumentReference<T> argument, T maxValue) where T: IComparable<T>
        {
            return IsLessThanOrEqualTo(argument, maxValue, null);
        }
    }
}
