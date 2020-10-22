using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETFRAMEWORK
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

        public IMember Member => _event;
        public abstract AccessorType AccessorType { get; }
        DeclarationType IAccessor.Declaration => OperationMethod.Declaration;
        AccessModifier IAccessor.AccessModifier => OperationMethod.AccessModifier;
        MemberInfo IReflected.ReflectedMember => OperationMethod.ReflectedMember;
        MethodInfo IReflected<MethodInfo>.ReflectedMember => OperationMethod.ReflectedMember;
    }
}