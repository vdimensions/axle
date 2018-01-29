using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public partial class MethodToken : MethodBaseToken<MethodInfo>, IEquatable<MethodToken>, IMethod
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type memberType;
        
        public bool Equals(MethodToken other) { return base.Equals(other); }
        public override bool Equals(object obj) { return obj is MethodToken && base.Equals(obj); }

        public override int GetHashCode() { return unchecked(base.GetHashCode()); }

        public object Invoke(object target, params object[] args) { return ReflectedMember.Invoke(target, args); }

        public IGenericMethod MakeGeneric(params Type[] types)
        {
            if (!this.HasGenericDefinition)
            {
                throw new InvalidOperationException("Cannot make a generic method - method does not accept type parameters.");
            }
            if (this is GenericMethodToken)
            {
                throw new InvalidOperationException(
                    "This method is already generic. Cannot make a generic method out of a generic method. Perhaps you need a cast to `IGenericMethod` instead?");
            }
            return new GenericMethodToken(this, types);
        }

        public override Type MemberType => memberType;
        public bool IsGeneric => ReflectedMember.IsGenericMethod;
        public bool HasGenericDefinition => ReflectedMember.IsGenericMethodDefinition;
        public Type ReturnType => ReflectedMember.ReturnType;
    }
}