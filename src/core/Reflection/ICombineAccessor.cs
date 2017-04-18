using System;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface ICombineAccessor : IAccessor
    {
        void AddDelegate(object target, Delegate handler);
    }
}