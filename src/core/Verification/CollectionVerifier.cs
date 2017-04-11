using System;
using System.Collections;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="ICollection" />.
    /// </summary>
    /// <seealso cref="ICollection"/>
    public static class CollectionVerifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotEmpty<T>(this ArgumentReference<T> argument, string message) where T: ICollection
        {
            if (argument.IsNotNull().Value.Count == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty collection.", argument.Name), argument.Name);
            }
            return argument;
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotEmpty<T>(this ArgumentReference<T> argument) where T: ICollection { return IsNotEmpty(argument, null); }
    }
}
