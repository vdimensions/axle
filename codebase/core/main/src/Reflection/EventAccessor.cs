using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class EventAccessor : IAccessor, IReflected<MethodInfo>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly EventToken _event;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly MethodToken OperationMethod;

        protected EventAccessor(EventToken @event, MethodToken operationMethod)
        {
            _event = @event;
            OperationMethod = operationMethod;
        }

        DeclarationType IAccessor.Declaration => OperationMethod.Declaration;
        AccessModifier IAccessor.AccessModifier => OperationMethod.AccessModifier;
        public IMember Member => _event;
        public abstract AccessorType AccessorType { get; }
        MemberInfo IReflected.ReflectedMember => OperationMethod.ReflectedMember;
        MethodInfo IReflected<MethodInfo>.ReflectedMember => OperationMethod.ReflectedMember;
    }
}