﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Axle.Reflection
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    //[Maturity(CodeMaturity.Stable)]
    public sealed class GenericMethodToken : MethodToken, IGenericMethod
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IMethod _rawMethod;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<Type> _genericArguments;

        internal GenericMethodToken(MethodToken rawMethod, params Type[] args) : base(rawMethod.ReflectedMember.MakeGenericMethod(args))
        {
            _rawMethod = rawMethod;
            _genericArguments = args;
        }

        public Type[] GenericArguments => _genericArguments.ToArray();
        public IMethod RawMethod => _rawMethod;
    }
}