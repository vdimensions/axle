#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public class AdaptiveComparer<T1, T2> : IComparer<T1>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly Func<T1, T2> adaptFunc;
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly IComparer<T2> actualComparer;
        
        public AdaptiveComparer(Func<T1, T2> adaptFunc, IComparer<T2> comparer)
        {
            this.adaptFunc = adaptFunc.VerifyArgument(nameof(adaptFunc)).IsNotNull();
            this.actualComparer = comparer.VerifyArgument(nameof(comparer)).IsNotNull().Value;
        }
        public AdaptiveComparer(Func<T1, T2> adaptFunc) : this(adaptFunc, Comparer<T2>.Default){}

        public int Compare(T1 x, T1 y) { return actualComparer.Compare(adaptFunc(x), adaptFunc(y)); }
    }
}
#endif