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
            _property = Verifier.IsNotNull(Verifier.VerifyArgument(property, nameof(property)));
            OperationMethod = Verifier.IsNotNull(Verifier.VerifyArgument(operationMethod, nameof(operationMethod)));
        }

        public IMember Member => _property;
        public abstract AccessorType AccessorType { get; }
        MemberInfo IReflected.ReflectedMember => OperationMethod.ReflectedMember;
        MethodInfo IReflected<MethodInfo>.ReflectedMember => OperationMethod.ReflectedMember;
        DeclarationType IAccessor.Declaration => OperationMethod.Declaration;
        AccessModifier IAccessor.AccessModifier => OperationMethod.AccessModifier;
    }
}