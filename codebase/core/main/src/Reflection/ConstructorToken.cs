using System;
using System.Reflection;

namespace Axle.Reflection
{
    /// <summary>
    /// The default <see cref="IConstructor"/> implementation. Serves as a wrapper around <see cref="ConstructorInfo"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ConstructorToken : MethodBaseToken<ConstructorInfo>, IEquatable<ConstructorToken>, IConstructor
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ConstructorToken"/> type using the provided constructor
        /// <paramref name="info" />.
        /// </summary>
        /// <param name="info">
        /// A <see cref="ConstructorInfo"/> instance representing the reflected constructor.
        /// </param>
        public ConstructorToken(ConstructorInfo info) : base(info) { }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is ConstructorToken && base.Equals(obj);
        /// <inheritdoc />
        public bool Equals(ConstructorToken other) => base.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => base.GetHashCode();

        /// <inheritdoc />
        public object Invoke(params object[] args) => ReflectedMember.Invoke(args);
        object IInvokable.Invoke(object target, params object[] args) => Invoke(args);

        /// <summary>
        /// The <see cref="Type"/> declaring the current <see cref="IConstructor"/> instance.
        /// Same as <see cref="MemberTokenBase{T}.DeclaringType"/>.
        /// </summary>
        public override Type MemberType => DeclaringType;
    }
}
