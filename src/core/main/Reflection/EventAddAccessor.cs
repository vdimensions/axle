using System;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    internal sealed class EventAddAccessor : EventAccessor, ICombineAccessor
    {
        public EventAddAccessor(EventToken @event, MethodToken operationMethod) : base(@event, operationMethod) { }

        public void AddDelegate(object target, Delegate handler) { OperationMethod.Invoke(target, handler); }

        public override AccessorType AccessorType { get { return AccessorType.Add; } }
    }
}