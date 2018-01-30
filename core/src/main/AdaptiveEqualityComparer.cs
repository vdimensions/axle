#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public class AdaptiveEqualityComparer<T1, T2> : IEqualityComparer<T1>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly Func<T1, T2> _adaptFunc;
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly IEqualityComparer<T2> _actualComparer;

        public AdaptiveEqualityComparer(Func<T1, T2> adaptFunc, IEqualityComparer<T2> comparer)
        {
            _adaptFunc = adaptFunc.VerifyArgument(nameof(adaptFunc)).IsNotNull();
            _actualComparer = comparer.VerifyArgument(nameof(comparer)).IsNotNull().Value;
        }
        public AdaptiveEqualityComparer(Func<T1, T2> adaptFunc) : this(adaptFunc, EqualityComparer<T2>.Default) { }
        
        public bool Equals(T1 x, T1 y) { return _actualComparer.Equals(_adaptFunc(x), _adaptFunc(y)); }
        
        public int GetHashCode(T1 obj) { return _actualComparer.GetHashCode(_adaptFunc(obj)); }
    }
}
#endif