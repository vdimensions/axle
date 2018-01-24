using System;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle
{
    #if !netstandard
    [Serializable]
    #endif
    public class AdaptiveEqualityComparer<T1, T2> : IEqualityComparer<T1>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly Func<T1, T2> adaptFunc;
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly IEqualityComparer<T2> actualComparer;

        public AdaptiveEqualityComparer(Func<T1, T2> adaptFunc, IEqualityComparer<T2> comparer)
        {
            this.adaptFunc = adaptFunc.VerifyArgument(nameof(adaptFunc)).IsNotNull();
            this.actualComparer = comparer.VerifyArgument(nameof(comparer)).IsNotNull().Value;
        }
        public AdaptiveEqualityComparer(Func<T1, T2> adaptFunc) : this(adaptFunc, EqualityComparer<T2>.Default) { }
        
        public bool Equals(T1 x, T1 y) { return actualComparer.Equals(adaptFunc(x), adaptFunc(y)); }
        
        public int GetHashCode(T1 obj) { return actualComparer.GetHashCode(adaptFunc(obj)); }
    }
}