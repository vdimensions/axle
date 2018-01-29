using System;


namespace Axle.Reflection
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    internal sealed class EventRemoveAccessor : EventAccessor, IRemoveAccessor
    {
        public EventRemoveAccessor(EventToken @event, MethodToken operationMethod) : base(@event, operationMethod) { }

        public void RemoveDelegate(object target, Delegate handler) { OperationMethod.Invoke(target, handler); }

        public override AccessorType AccessorType => AccessorType.Remove;
    }
}