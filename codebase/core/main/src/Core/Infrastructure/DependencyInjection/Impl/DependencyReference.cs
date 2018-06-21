//using System;
//using System.Diagnostics;
//
//using Axle.Extensions.Object;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Impl
//{
//    internal sealed class DependencyReference : IDependencyReference, IEquatable<IDependencyReference>
//    {
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly Type _type;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly IResolvable _resolvable;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly string _name;
//
//        public DependencyReference(Type type, string name, IResolvable resolvable)
//        {
//            _type = type;
//            _name = name;
//            _resolvable = resolvable;
//        }
//
//        public bool Equals(IDependencyReference other)
//        {
//            return other != null && _type == other.Type && StringComparer.Ordinal.Equals(_name, other.Name);
//        }
//        public override int GetHashCode() => this.CalculateHashCode(Type, Name);
//
//        public Type Type => _type;
//        public string Name => _name;
//        public object Value => _resolvable.Value;
//    }
//}