using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// A struct that represents a reference to an argument for a method or constructor. 
    /// The argument reference is usually represented by its name (as defined in the respective method/constructor) and the value passed to it. 
    /// </summary>
    public partial struct ArgumentReference<T>
    {
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        private ArgumentReference<T> IsOfTypeUnchecked(Type expectedType)
        {
            var actualType = Value.GetType();
            if (!expectedType.IsAssignableFrom(actualType))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, Name);
            }
            return this;
        }
    }
}