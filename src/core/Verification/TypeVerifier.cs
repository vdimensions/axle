using System;
using System.Diagnostics;


namespace Axle.Verification
{
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

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> Is(this ArgumentReference<Type> argument, Type expectedType)
        {
            return IsUnchecked(argument.VerifyArgument("this").IsNotNull().Value, expectedType.VerifyArgument("expectedType").IsNotNull());
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<Type> Is<TExpected>(this ArgumentReference<Type> argument)
        {
            return IsUnchecked(argument.VerifyArgument("this").IsNotNull().Value, typeof(TExpected));
        }
    }
}
