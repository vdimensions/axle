using System;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IRemoveAccessor : IAccessor
    {
        void RemoveDelegate(object target, Delegate handler);
    }
}