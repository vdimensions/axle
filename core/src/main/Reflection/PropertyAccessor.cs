using System;
using System.Diagnostics;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    #if !netstandard
    [Serializable]
    #endif
    internal abstract class PropertyAccessor : IAccessor, IReflected<MethodInfo>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyToken property;
        protected readonly MethodToken OperationMethod;

        protected PropertyAccessor(PropertyToken property, MethodToken operationMethod)
        {
            this.property = property.VerifyArgument(nameof(property)).IsNotNull();
            this.OperationMethod = operationMethod.VerifyArgument(nameof(operationMethod)).IsNotNull();
        }

        public IMember Member { get { return property; } }
        public abstract AccessorType AccessorType { get; }
        MemberInfo IReflected.ReflectedMember { get { return OperationMethod.ReflectedMember; } }
        MethodInfo IReflected<MethodInfo>.ReflectedMember { get { return OperationMethod.ReflectedMember; } }
        DeclarationType IAccessor.Declaration { get { return OperationMethod.Declaration; } }
        AccessModifier IAccessor.AccessModifier { get { return OperationMethod.AccessModifier; } }
    }
}