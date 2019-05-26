using System;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ConstructorToken : MethodBaseToken<ConstructorInfo>, IEquatable<ConstructorToken>, IConstructor
    {
        public ConstructorToken(ConstructorInfo info) : base(info) { }

        public override bool Equals(object obj) => obj is ConstructorToken && base.Equals(obj);
        public bool Equals(ConstructorToken other) => base.Equals(other);

        public override int GetHashCode() => base.GetHashCode();

        /// <inheritdoc />
        public object Invoke(params object[] args) => ReflectedMember.Invoke(args);
        object IInvokable.Invoke(object target, params object[] args) => Invoke(args);

        public override Type MemberType => DeclaringType;
    }
}
