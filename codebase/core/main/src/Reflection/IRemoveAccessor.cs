using System;


namespace Axle.Reflection
{
    public interface IRemoveAccessor : IAccessor
    {
        void RemoveDelegate(object target, Delegate handler);
    }
}