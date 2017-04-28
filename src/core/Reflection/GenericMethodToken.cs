using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    //[Maturity(CodeMaturity.Stable)]
    public sealed class GenericMethodToken : MethodToken, IGenericMethod
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IMethod rawMethod;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<Type> genericArguments;

        internal GenericMethodToken(MethodToken rawMethod, params Type[] args) : base(rawMethod.ReflectedMember.MakeGenericMethod(args))
        {
            this.rawMethod = rawMethod;
            this.genericArguments = args;
        }

        public Type[] GenericArguments { get { return genericArguments.ToArray(); } }
        public IMethod RawMethod { get { return rawMethod; } }
    }
}