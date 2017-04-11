using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of array types.
    /// </summary>
    public static class ArrayVerifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T[]> IsNotEmpty<T>(this ArgumentReference<T[]> argument, string message)
        {
            if (argument.IsNotNull().Value.Length == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty array.", argument.Name), argument.Name);
            }
            return argument;
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T[]> IsNotEmpty<T>(this ArgumentReference<T[]> argument) { return IsNotEmpty(argument, null); }
    }
}
