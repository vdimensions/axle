#if NETSTANDARD || NET35_OR_NEWER
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

        public bool Equals(MethodToken other) { return base.Equals(other); }
        public override bool Equals(object obj) { return obj is MethodToken && base.Equals(obj); }

        public override int GetHashCode() { return base.GetHashCode(); }

        public object Invoke(object target, params object[] args) { return ReflectedMember.Invoke(target, args); }

        public IGenericMethod MakeGeneric(params Type[] types)
        {
            if (!HasGenericDefinition)
            {
                throw new InvalidOperationException("Cannot make a generic method - method does not accept type parameters.");
            }
            if (this is GenericMethodToken)
            {
                // TODO: WHY NOT?
                throw new InvalidOperationException(
                    "This method is already generic. Cannot make a generic method out of a generic method. Perhaps you need a cast to `IGenericMethod` instead?");
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
#endif