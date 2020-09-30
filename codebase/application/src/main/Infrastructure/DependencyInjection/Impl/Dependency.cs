//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//
//using Axle.Core.Infrastructure.DependencyInjection.Sdk;
//using Axle.Extensions.Object;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Impl
//{
//    internal sealed class Dependency : IDependency
//    {
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly Type _requestedType;
//
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly string _declaredName;
//
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly string _inferredName;
//
//        [DebuggerStepThrough]
//        public Dependency(Type requestedType, string declaredName) : this(requestedType, declaredName, null) { }
//        [DebuggerStepThrough]
//        public Dependency(Type requestedType, string declaredName, string suggestedName)
//        {
//            _requestedType = requestedType;
//            _declaredName = declaredName;
//            _inferredName = suggestedName ?? string.Empty;
//        }
//
//        public Type RequestedType => _requestedType;
//        public string DeclaredName => _declaredName;
//        public string InferredName => _inferredName;
//        public bool IsOptional => false;
//
//        public IEnumerable<Attribute> GetAttributes() => new Attribute[0];
//
//        [DebuggerStepThrough]
//        public bool Equals(IDependency other)
//        {
//            var cmp = StringComparer.Ordinal;
//            return other != null 
//                && _requestedType == other.RequestedType
//                && cmp.Equals(_declaredName, other.DeclaredName)
//                && cmp.Equals(_inferredName, other.InferredName);
//        }
//        [DebuggerStepThrough]
//        public override bool Equals(object obj) => obj is IDependency d && Equals(d);
//
//        [DebuggerStepThrough]
//        public override int GetHashCode() => this.CalculateHashCode(_requestedType, _declaredName, _inferredName);
//    }
//}