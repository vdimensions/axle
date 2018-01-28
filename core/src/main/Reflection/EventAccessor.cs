using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if !netstandard
    [Serializable]
    #endif
    internal abstract class EventAccessor : IAccessor, IReflected<MethodInfo>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly EventToken @event;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly MethodToken OperationMethod;

        protected EventAccessor(EventToken @event, MethodToken operationMethod)
        {
            this.@event = @event;
            this.OperationMethod = operationMethod;
        }

        DeclarationType IAccessor.Declaration { get { return OperationMethod.Declaration; } }
        AccessModifier IAccessor.AccessModifier { get { return OperationMethod.AccessModifier; } }
        public IMember Member { get { return @event; } }
        public abstract AccessorType AccessorType { get; }
        MemberInfo IReflected.ReflectedMember { get { return OperationMethod.ReflectedMember; } }
        MethodInfo IReflected<MethodInfo>.ReflectedMember { get { return OperationMethod.ReflectedMember; } }
    }
}