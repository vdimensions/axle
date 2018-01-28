using System;
using System.Reflection;


namespace Axle.Reflection
{
    #if !netstandard
    [Serializable]
    #endif
    //[Maturity(CodeMaturity.Stable)]
    public sealed class ConstructorToken : MethodBaseToken<ConstructorInfo>, IEquatable<ConstructorToken>, IConstructor
    {
        public ConstructorToken(ConstructorInfo info) : base(info) { }

        public override bool Equals(object obj) { return obj is ConstructorToken && base.Equals(obj); }
        public bool Equals(ConstructorToken other) { return base.Equals(other); }

        public override int GetHashCode() { return base.GetHashCode(); }

        public object Invoke(params object[] args) { return ReflectedMember.Invoke(args); }
        object IInvokable.Invoke(object target, params object[] args) { return this.Invoke(args); }

        public override Type MemberType { get { return DeclaringType; } }
    }
}