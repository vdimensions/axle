#if NETSTANDARD || NET35_OR_NEWER
using System.Diagnostics;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class PropertyAccessor : IAccessor, IReflected<MethodInfo>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyToken _property;
        protected readonly MethodToken OperationMethod;

        protected PropertyAccessor(PropertyToken property, MethodToken operationMethod)
        {
            _property = property.VerifyArgument(nameof(property)).IsNotNull();
            OperationMethod = operationMethod.VerifyArgument(nameof(operationMethod)).IsNotNull();
        }

        public IMember Member => _property;
        public abstract AccessorType AccessorType { get; }
        MemberInfo IReflected.ReflectedMember => OperationMethod.ReflectedMember;
        MethodInfo IReflected<MethodInfo>.ReflectedMember => OperationMethod.ReflectedMember;
        DeclarationType IAccessor.Declaration => OperationMethod.Declaration;
        AccessModifier IAccessor.AccessModifier => OperationMethod.AccessModifier;
    }
}
#endif