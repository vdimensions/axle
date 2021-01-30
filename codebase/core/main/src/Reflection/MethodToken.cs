using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class MethodToken : MethodBaseToken<MethodInfo>, IEquatable<MethodToken>, IMethod
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _memberType;

        /// <summary>
        /// Creates a new <see cref="MethodToken"/> instance using the provided <paramref name="info"/>.
        /// </summary>
        /// <param name="info">
        /// A <see cref="MethodInfo"/> object containing the reflected information for the represented method.
        /// </param>
        public MethodToken(MethodInfo info) : base(info)
        {
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            _memberType = info.ReturnType;
            #else
            _memberType = (ReflectedMember = info).ReturnType;
            #endif
        }

        public bool Equals(MethodToken other) => base.Equals(other);
        public override bool Equals(object obj) => obj is MethodToken && base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public object Invoke(object target, params object[] args) => ReflectedMember.Invoke(target, args);

        public IGenericMethod MakeGeneric(params Type[] types)
        {
            if (!HasGenericDefinition)
            {
                throw new InvalidOperationException("Cannot make a generic method - method does not accept type parameters.");
            }
            if (this is IPartiallyGenericMethod pgm)
            {
                var concatenatedTypes = new Type[pgm.GenericArguments.Length + types.Length];
                pgm.GenericArguments.CopyTo(concatenatedTypes, 0);
                types.CopyTo(concatenatedTypes, pgm.GenericArguments.Length);
                return pgm.RawMethod.MakeGeneric(concatenatedTypes);
            }
            if (this is IGenericMethod gm)
            {
                return gm.RawMethod.MakeGeneric(types);
            }
            return new GenericMethodToken(this, types);
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        #else
        public override MethodInfo ReflectedMember { get; }
        #endif
        public override Type MemberType => _memberType;
        public bool IsGeneric => ReflectedMember.IsGenericMethod;
        public bool HasGenericDefinition => ReflectedMember.IsGenericMethodDefinition;
        public Type ReturnType => ReflectedMember.ReturnType;
    }
}