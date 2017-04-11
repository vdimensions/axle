using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="string" />.
    /// </summary>
    /// <seealso cref="string"/>
    public static class StringVerifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotEmpty(this ArgumentReference<string> argument, string message)
        {
            if (Verifier.IsNotNull(argument).Value.Length == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty string.", argument.Name), argument.Name);
            }
            return argument;
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotEmpty(this ArgumentReference<string> argument) { return IsNotEmpty(argument, null); }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotNullOrEmpty(this ArgumentReference<string> argument)
        {
            return IsNotEmpty(Verifier.IsNotNull(argument));
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotNullOrEmpty(this ArgumentReference<string> argument, string emptyMessage)
        {
            return IsNotEmpty(Verifier.IsNotNull(argument), emptyMessage);
        }
    }
}
